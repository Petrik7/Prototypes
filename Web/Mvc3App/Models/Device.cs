using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3App.Models
{

    public class Device
    {
        private string _id;
        private string _name;
        private string _description;
        private string _idCode;
        private string _url;
        private string _manufacture;
        private string _username;
        private string _pass;
        private string _type;
        private string _resolution;
        private string _fps;
        private string _videoFormat;
        private string _failoverMovability;
        private string _supportDirectConnect;
        private string _audioInput;
        private string _audiOutput;

        public Device()
        {             
            _id = "4";
            _name = "My_Camera_Name";
            _description = "My_Camera_Description";
            _idCode = string.Empty;
            _url = "192.168.1.123";
            _manufacture = "Axis";
            _username = "rooot";
            _pass = "Asnfohrkd";
            _type = "Fixed";
            _resolution = "1024 x 768";
            _fps = "30";
            _videoFormat = "H.264";
            _failoverMovability = string.Empty;
            _supportDirectConnect = string.Empty;
            _audioInput = string.Empty;
            _audiOutput = string.Empty;
        }

        public string ID { get {return _id;;} /*set;*/ }
        public string Name { get {return _name;;} /*set;*/ }
        public string Description { get {return _description;;} /*set;*/ }
        public string IdCode { get {return _idCode;;} /*set;*/ }
        public string Url { get {return _url;;} /*set;*/ }
        public string Manufacture { get {return _manufacture;;} /*set;*/ }
        public string Username { get {return _username;;} /*set;*/ }
        public string Pass { get {return _pass;;} /*set;*/ }
        public string Type { get {return _type;;} /*set;*/ }
        public string Resolution { get {return _resolution;;} /*set;*/ }
        public string FPS { get {return _fps;;} /*set;*/ }
        public string VideoFormat { get {return _videoFormat;;} /*set;*/ }
        public string FailoverMovability { get {return _failoverMovability;;} /*set;*/ }
        public string SupportDirectConnect { get {return _supportDirectConnect;;} /*set;*/ }
        public string AudioInput { get {return _audioInput;;} /*set;*/ }
        public string AudiOutput { get {return _audiOutput;;} /*set;*/ }
    }

    public class DBDevice
    {
        public static Device GetDevice(int deviceID)
        {
            // Select from DB prepare and return Device
            return new Device();
        }
    }

    //public class MovieDBContext : DbContext
    //{
    //    public DbSet<MovieDB> Movies { get {return ;;} /*set;*/ }
    //}
}
