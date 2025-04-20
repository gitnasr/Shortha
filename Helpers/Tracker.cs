using DeviceDetectorNET;
using IPinfo;

public class TrackerBuilder
{
    private readonly DeviceDetector _deviceDetector;
    private readonly Tracker _tracker;
    private readonly IPinfoClient _client;

    public TrackerBuilder(string userAgent, string ipAddress, IPinfoClient client)
    {
        _deviceDetector = new DeviceDetector(userAgent);
        _deviceDetector.Parse();

        _client = client;

        _tracker = new Tracker
        {
            UserAgent = userAgent

        };
    }

    public TrackerBuilder WithBrowser()
    {
        _tracker.BrowserName = _deviceDetector.GetClient().Match.Name;
        _tracker.BrowserVersion = _deviceDetector.GetClient().Match.Version;
        return this;
    }

    public TrackerBuilder WithOs()
    {
        _tracker.OSName = _deviceDetector.GetOs().Match.Name;
        return this;
    }

    public TrackerBuilder WithBrand()
    {
        _tracker.DeviceBrand = _deviceDetector.GetBrandName();
        return this;
    }

    public TrackerBuilder WithModel()
    {
        _tracker.DeviceType = _deviceDetector.GetModel();
        return this;
    }

    public TrackerBuilder WithIpAddress()
    {
        if (_tracker.IpAddress != "Unknown")
        {


            var ipInfo = _client.IPApi.GetDetails(_tracker.IpAddress);
            if (ipInfo != null)
            {
                _tracker.IpAddress = ipInfo.IP;
                _tracker.Country = ipInfo.Country;
                _tracker.Region = ipInfo.Region;
                _tracker.City = ipInfo.City;
            }
        }

        return this;

    }

    public Tracker Build()
    {
        return _tracker;
    }
}

public class Tracker
{
    public string BrowserName { get; set; }
    public string BrowserVersion { get; set; }
    public string OSName { get; set; }
    public string DeviceBrand { get; set; }
    public string DeviceType { get; set; }
    public string UserAgent { get; set; }

    private string _ipAddress;
    public string Country { get; set; }
    public string Region { get; set; }
    public string City { get; set; }

    public string IpAddress
    {
        get
        {
            if (string.IsNullOrEmpty(_ipAddress) || _ipAddress == "::1")
            {
                return "Unknown";
            }
            return _ipAddress;
        }
        set
        {
            _ipAddress = value;
        }
    }
}
