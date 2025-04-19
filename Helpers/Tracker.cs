using DeviceDetectorNET;
using DeviceDetectorNET.Results;

namespace Shortha.Helpers
{
    public class Tracker
    {

        private readonly DeviceDetector _deviceDetector;
        public Tracker(string userAgent)
        {
            _deviceDetector = new DeviceDetector(userAgent);
            _deviceDetector.Parse();
        }

        public string GetBrowser()
        {
            var r = _deviceDetector.GetClient();
            return r.Match.Name;
        }
        public string GetOs()
        {
            var r = _deviceDetector.GetOs();
            return r.Match.Name;
        }

        public string GetDevice()
        {
            var r = _deviceDetector.GetDeviceName();
            return r;
        }

        public string GetBrand()
        {
            var r = _deviceDetector.GetBrandName();
            return r;
        }
        public string GetModel()
        {
            var r = _deviceDetector.GetModel();
            return r;
        }
        public string GetVersion()
        {
            var r = _deviceDetector.GetClient();
            return r.Match.Version;
        }
    }
}
