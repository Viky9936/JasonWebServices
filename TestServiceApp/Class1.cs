
public class Rootobject
{
    public Class1[] Property1 { get; set; }
}

public class Class1
{
    public Employee employee { get; set; }
    public Addr addr { get; set; }
    public Sal sal { get; set; }
}

public class Employee
{
    public int emp_id { get; set; }
    public string emp_name { get; set; }
    public int salary { get; set; }
}

public class Addr
{
    public string address1 { get; set; }
    public string address2 { get; set; }
    public string city { get; set; }
}

public class Sal
{
    public int hourly_wages { get; set; }
    public string payment_Date { get; set; }
}
