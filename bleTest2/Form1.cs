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

namespace bleTest2
{
    public partial class Form1 : Form
    {
        static DeviceInformation device = null;
        static public List<string> items = new List<string>();

        public delegate int updateDelegate();

        

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
            while (true)
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

            }

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


            if (args.Name == "Nugawinder")
            {
                Console.WriteLine(args.Name);
                device = args;
                Console.WriteLine("id : " + args.Id);
                items.Add(args.Name);

            }

            //throw new NotImplementedException();
        }

        private void updateDeviceList()
        {
            /*listRec.DataSource = items;*/
        }

        private void btnRst_Click(object sender, EventArgs e)
        {
            listRec.DataSource = items;
            for(int i = 0; i < items.Count; i++)
            {
                /*listRec.Items = items[i];*/
            }

        }


    }
}
