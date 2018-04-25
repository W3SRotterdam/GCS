public class ErrorResult {
    public Error error { get; set; }
}

public class Error {
    public Error1[] errors { get; set; }
    public int code { get; set; }
    public string message { get; set; }
}

public class Error1 {
    public string domain { get; set; }
    public string reason { get; set; }
    public string message { get; set; }
    public string extendedHelp { get; set; }
}
