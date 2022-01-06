using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace bleTest2
{
    public partial class frmMeasure : Form
    {
        static DeviceInformation device = null;
        static public List<string> items = new List<string>();

        public delegate void update_rst();

        public Guid actUuid;
        public static GattCharacteristic actGattCharacteristic;

        public string itemK = "BluetoothLE#BluetoothLEe8:48:b8:c8:20:00-e4:c8:55:e8:84:28";
        public int TotalSeconds = 0;


        public frmMeasure()
        {
            InitializeComponent();
        }

        Timer timer = new Timer();
        Timer timer2 = new Timer();
        Timer timer3 = new Timer();

        private void setTimer()
        {
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            

            ++TotalSeconds;

            int seconds = TotalSeconds;
            if (seconds <= 6)
            {
                
                lvlCnt.Text = seconds.ToString();
                

            }
            else
            {
                timer.Stop();
            }
        }


        public static string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

        public static DeviceWatcher deviceWatcher =
                    DeviceInformation.CreateWatcher(
                            BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                            requestedProperties,
                            DeviceInformationKind.AssociationEndpoint);

        static async Task Main()
        {
            // Query for extra properties you want returned
            /*            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

                        DeviceWatcher deviceWatcher =
                                    DeviceInformation.CreateWatcher(
                                            BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                                            requestedProperties,
                                            DeviceInformationKind.AssociationEndpoint);*/

            DeviceWatcher deviceWatcherCopy = deviceWatcher;

            // Register event handlers before starting the watcher.
            // Added, Updated and Removed are required to get all nearby devices
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;

            // Start the watcher.
            deviceWatcher.Start();
            /*deviceWatcher.Stop();*/



        }

        private static void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
        {
            /*throw new NotImplementedException();*/
        }

        private static void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            /*throw new NotImplementedException();*/
        }

        private static void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            Console.WriteLine("종료확인~~~~~~~~~~~~~~~~~~~~~~~~~");
            /*throw new NotImplementedException();*/
        }

        private static void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            /*throw new NotImplementedException();*/
        }

        private static void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            update_rst my_update_rst;


            if (args.Name == "Nugawinder")
            {
                Console.WriteLine("장치 명 : " + args.Name);
                device = args;
                Console.WriteLine("장치 id : " + args.Id);
                /*items.Add(args.Name+"("+args.Id+")");*/
                items.Add(args.Id);
                /*my_update_rst = updateRst;*/
            }

            //throw new NotImplementedException();
        }

        public GattCharacteristic characteristicTemp;
        public BluetoothLEDevice bluetoothLeDeviceTemp;
        public GattDeviceService serviceTemp;
        
        //----------------------------------------------------------------------------------------------

        private async void AutoSet()
        {
            //Console.WriteLine($"click 인덱스 ====> {listRec.SelectedIndex}");
            //Console.WriteLine($"click 아이템 ====> {listRec.SelectedItem}");
            //int idx = listRec.SelectedIndex;
            //string itm = listRec.SelectedItem.ToString();
            //string itemK = "BluetoothLE#BluetoothLEe8:48:b8:c8:20:00-e4:c8:55:e8:84:28";            

            BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(itemK);
            GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

            bluetoothLeDeviceTemp = bluetoothLeDevice;

            if (result.Status == GattCommunicationStatus.Success)
            {
                var services = result.Services;

                foreach (var service in services)
                {
                    serviceTemp = service;
                    Console.WriteLine($"연결상태 확인 start : {service?.Session.SessionStatus}");
                    Console.WriteLine($"{service.Uuid}");
                    Console.WriteLine("→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→");
                    GattCharacteristicsResult cResult = await service.GetCharacteristicsAsync();

                    if (cResult.Status == GattCommunicationStatus.Success)
                    {
                        var characteristics = cResult.Characteristics;
                        foreach (var characteristic in characteristics)
                        {
                            characteristicTemp = characteristic;
                            Console.WriteLine($"\t{characteristic.Uuid}");

                            GattCharacteristicProperties properties = characteristic.CharacteristicProperties;
                            if (properties.HasFlag(GattCharacteristicProperties.Notify))
                            {
                                Console.WriteLine("Notify property found");
                                GattCommunicationStatus status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                                if (status == GattCommunicationStatus.Success)
                                {
                                    characteristic.ValueChanged += Characteristic_ValueChanged;
                                    // Server has been informed of clients interest.
                                }
                            }
                            else if (properties.HasFlag(GattCharacteristicProperties.Write))
                            {


                                var writer = new DataWriter();
                                writer.WriteBytes(new byte[] { 0xCC });
                                                                Console.WriteLine($"\t\t [{characteristic.Uuid}] write property found!!");

                                if (!string.IsNullOrWhiteSpace(characteristic.Uuid.ToString()))
                                {
                                    string[] str_split = characteristic.Uuid.ToString().Split(new char[] { '-' });
                                    int len = str_split[0].Length;
                                    string chkCode = str_split[0].Substring(len - 2, 2);

                                    if (chkCode == "02")
                                    {
                                        GattCommunicationStatus statusWrite = await characteristic.WriteValueAsync(writer.DetachBuffer());
                                        if (statusWrite == GattCommunicationStatus.Success)
                                        {
                                            // Successfully wrote to device
                                            actGattCharacteristic = characteristic;
                                            actUuid = characteristic.Uuid;
                                            Console.WriteLine("Write !!!!!!");

                                        }
                                    }
                                }

                            }
                            else if (properties.HasFlag(GattCharacteristicProperties.WriteWithoutResponse))
                            {
                                Console.WriteLine($"\t\t [{characteristic.Uuid} ] WriteWithoutResponse property found");

                            }
                        }
                    }

                    //service.Dispose();
                }
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.AutoCloseOptions.Delay = 2000;
                args.Caption = "측정준비완료";
                args.Text = "스파이로미터를 힘껏 부세요.";
                //args.Buttons = new DialogResult[] { DialogResult.OK };
                XtraMessageBox.Show(args).ToString();
                writePulse();
                /*                bluetoothLeDevice.Dispose();
                                bluetoothLeDevice = null;*/
                /*bluetoothLeDevice.Dispose();
                GC.Collect();*/


            }
            else
            {
                MessageBox.Show("Pairing fail!!!");
                Console.WriteLine("Pairing fail");
            }

        }

        //---------------------------------------------------------------------------------------------

        private async void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            var reader = DataReader.FromBuffer(args.CharacteristicValue);
            byte[] buff = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(buff);
            Console.WriteLine($"byte size : {buff.Length}");

            var readTask = Task.Run(() =>
            {
                string hex = BitConverter.ToString(buff).Replace("-", " ") + " ";
                Console.WriteLine(string.Format("hex code : {0}", hex));
            });
            await readTask;
            setText(BitConverter.ToString(buff).Replace("-", " ") + " ");

        }

        private void setText(String text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler(delegate {
                    //if (text != null) txbRXHex.AppendText(text);

                }));
            }
            else
            {
                //sif (text != null) txbRXHex.AppendText(text);

            }
        }

        private void frmMeasure_Load(object sender, EventArgs e)
        {
            AutoSet();
        }


        private async void writePulse()
        {
            //int idx = listRec.SelectedIndex;
            //string itm = listRec.SelectedItem.ToString();

            //Console.WriteLine($"idx : {idx} 아이템 : {itm} ");
            GattCharacteristicProperties properties = actGattCharacteristic.CharacteristicProperties;
            if (properties.HasFlag(GattCharacteristicProperties.Write))
            {
                // This characteristic supports writing to it.
                var writer = new DataWriter();
                writer.WriteBytes(new byte[] { 0x4D, 0x4F, 0x52, 0x52 });
                /*writer.WriteBytes(new byte[] { 10 });*/
                Console.WriteLine($"\t\t [{actGattCharacteristic.Uuid}] write22 property found!!");

                if (!string.IsNullOrWhiteSpace(actGattCharacteristic.Uuid.ToString()))
                {
                    string[] str_split = actGattCharacteristic.Uuid.ToString().Split(new char[] { '-' });
                    int len = str_split[0].Length;
                    string chkCode = str_split[0].Substring(len - 2, 2);

                    if (chkCode == "02")
                    {
                        GattCommunicationStatus statusWrite = await actGattCharacteristic.WriteValueAsync(writer.DetachBuffer());
                        if (statusWrite == GattCommunicationStatus.Success)
                        {
                            // Successfully wrote to device                            
                            Console.WriteLine("Write22 !!!!!!");

                        }
                    }
                }


            }
        }


    }
}
