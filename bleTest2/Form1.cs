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

namespace bleTest2
{
    public partial class Form1 : Form
    {
        static DeviceInformation device = null;
        static public List<string> items = new List<string>();
        private object selectedCharacteristic;

        public delegate void update_rst();

        

        public Form1()
        {
            InitializeComponent();
            
        }




        private void btnScan_Click(object sender, EventArgs e)
        {
            Main();
        }

        static async Task Main()
        {
            // Query for extra properties you want returned
            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            DeviceWatcher deviceWatcher =
                        DeviceInformation.CreateWatcher(
                                BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                                requestedProperties,
                                DeviceInformationKind.AssociationEndpoint);

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
            /*while (true)
            {
                if (device == null)
                {
                    
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("PRess Any to pair with Wahoo Ticker");
                    Console.ReadKey();
                    BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(device.Id);
                    Console.WriteLine("Attempting to pair with device");
                    Console.WriteLine("장치 아이디 확인 : " + device.Id);
                    GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

                    if (result.Status == GattCommunicationStatus.Success)
                    {
                        Console.WriteLine("Pairing succeeded");
                        var services = result.Services;
                        foreach (var service in services)
                        {
                            Console.WriteLine(service.Uuid);
                        }
                    }
                    Console.WriteLine("Press Any Key to Exit application");
                    Console.ReadKey();
                    break;
                }
            }*/
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


        private void listRec_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine(sender.ToString());
        }

        private async void listRec_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"데이터 : {listRec.SelectedIndex}");
            Console.WriteLine($"데이터 : {listRec.SelectedItem}");
            int idx = listRec.SelectedIndex;
            string itm = listRec.SelectedItem.ToString();

            BluetoothLEDevice bluetoothLeDevice = await BluetoothLEDevice.FromIdAsync(itm);
            GattDeviceServicesResult result = await bluetoothLeDevice.GetGattServicesAsync();

            if(result.Status == GattCommunicationStatus.Success)
            {
                var services = result.Services;
                
                foreach(var service in services)
                {
                    Console.WriteLine($"{service.Uuid}");
                    Console.WriteLine("→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→→");
                    GattCharacteristicsResult cResult = await service.GetCharacteristicsAsync();

                    if (cResult.Status == GattCommunicationStatus.Success)
                    {
                        var characteristics = cResult.Characteristics;
                        foreach(var characteristic in characteristics)
                        {
                            
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
                                writer.WriteBytes(new byte[] { 10 });
                                writer.WriteBytes(new byte[] { 0x4D, 0x4F, 0x52, 0x52 });
                                writer.WriteBytes(new byte[] { 10 });
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
                }
                MessageBox.Show("Pairing Success!!");
            }
            else
            {
                MessageBox.Show("Pairing fail!!!");
                Console.WriteLine("Pairing fail");
            }

        }

        private void Characteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            var reader = DataReader.FromBuffer(args.CharacteristicValue);
            var value = reader.ReadByte();
            var hex = ToHex(value);
            Console.WriteLine($"{hex}");
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

    }
}
