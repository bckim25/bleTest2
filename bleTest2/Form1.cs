using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using System.Windows.Threading;
using Windows.Storage.Streams;
using DevExpress.XtraEditors;

namespace bleTest2
{
    public partial class Form1 : Form
    {
        static DeviceInformation device = null;
        static public List<string> items = new List<string>();

        public delegate void update_rst();

        public Guid actUuid;
        public static GattCharacteristic actGattCharacteristic;

        public string itemK = "BluetoothLE#BluetoothLE00:e0:4c:23:99:87-d5:43:22:33:d9:d0";



        public Form1()
        {
            InitializeComponent();
            
        }




        private void btnScan_Click(object sender, EventArgs e)
        {
            Main();
            
            //btnRst_Click();
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

        private void btnRst_Click(object sender, EventArgs e)
        {

            listRec.DataSource = items;
            for(int i = 0; i < items.Count; i++)
            {
                /*listRec.Items = items[i];*/
            }
            /*items.Clear();*/
        }

        private void btnRst_Click()
        {
            //listRec.DataSource = items;            
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"아이템===> {items[i]}");
                listRec.Items.Add(items[i]);
                
            }
        }



        public GattCharacteristic characteristicTemp;
        public BluetoothLEDevice bluetoothLeDeviceTemp;
        public GattDeviceService serviceTemp;

        private async void listRec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"click 인덱스 ====> {listRec.SelectedIndex}");
            Console.WriteLine($"click 아이템 ====> {listRec.SelectedItem}");
            int idx = listRec.SelectedIndex;
            string itm = listRec.SelectedItem.ToString();
            Console.WriteLine($"★★★★★★ 아이템이름===> {itm}");

            BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(itm);
            GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

            bluetoothLeDeviceTemp = bluetoothLeDevice;

            if (result.Status == GattCommunicationStatus.Success)
            {
                var services = result.Services;
                
                foreach(var service in services)
                {
                    serviceTemp = service;
                    Console.WriteLine($"연결상태 확인 start : {service?.Session.SessionStatus}");
                    Console.WriteLine($"{service.Uuid}");
                    Console.WriteLine("→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→");
                    GattCharacteristicsResult cResult = await service.GetCharacteristicsAsync();
                    
                    if (cResult.Status == GattCommunicationStatus.Success)
                    {
                        var characteristics = cResult.Characteristics;
                        foreach(var characteristic in characteristics)
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
                            }else if (properties.HasFlag(GattCharacteristicProperties.Write))
                            {
                                

                                var writer = new DataWriter();
                                writer.WriteBytes(new byte[] { 0xCC });
                                /*writer.WriteBytes(new byte[] { 10 });*/
                                /*writer.WriteBytes(new byte[] { 0x4D, 0x4F, 0x52, 0x52 });*/
                                /*writer.WriteBytes(new byte[] { 10 });*/
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




            }
            else
            {
                MessageBox.Show("Pairing fail!!!");
                Console.WriteLine("Pairing fail");
            }

        }
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
                                /*writer.WriteBytes(new byte[] { 10 });*/
                                /*writer.WriteBytes(new byte[] { 0x4D, 0x4F, 0x52, 0x52 });*/
                                /*writer.WriteBytes(new byte[] { 10 });*/
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
                args.Caption = "연결완료";
                args.Text = "연결되었습니다..";
                args.Buttons = new DialogResult[] { DialogResult.OK };
                XtraMessageBox.Show(args).ToString();
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
                    if (text != null) txbRXHex.AppendText(text);
                    
                }));
            }
            else
            {
                if (text != null) txbRXHex.AppendText(text);

            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            listRec.DataSource = null;
            listRec.Items.Clear();
        }

        public string ToHex(int i)
        { // 대문자 X일 경우 결과 hex값이 대문자로 나온다.
          //
          string hex = i.ToString("x");
          if (hex.Length % 2 != 0) 
            { 
                hex = "0" + hex; 
            } 
            return hex; 
        }

        private async void btnMeasure_Click(object sender, EventArgs e)
        {
            int idx = listRec.SelectedIndex;
            string itm = listRec.SelectedItem.ToString();

            Console.WriteLine($"idx : {idx} 아이템 : {itm} ");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            //disconnect 테스트중
            First:
            if (bluetoothLeDeviceTemp != null)
            {
                serviceTemp.Dispose();
                bluetoothLeDeviceTemp.Dispose();
                GC.Collect();
                string msg = serviceTemp?.Session.SessionStatus.ToString();
                Console.WriteLine($"메세지 ==> {msg}");
                Console.WriteLine($"연결상태 확인 btnClose : {serviceTemp?.Session.SessionStatus}");
                if(msg == "Active")
                {
                    goto First;
                }
            }

            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.AutoCloseOptions.Delay = 2000;
            args.Caption = "페어링종료";
            args.Text = "종료되었습니다..";
            args.Buttons = new DialogResult[] { DialogResult.OK };
            XtraMessageBox.Show(args).ToString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AutoSet();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoSet();
        }

        private async void btnMeasure2_Click(object sender, EventArgs e)
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
