using ShipmentService.Constants;

namespace ShipmentService.Services.Implementation;

public class CalculatePriority
{
    // Method to calculate priority...
    public PriorityLevel calculatePriority(DateTime sendingDate, DateTime receivingDate)
    {
        Console.WriteLine("Calculating priority method is triggered");
        Console.WriteLine("Parameters check:    " + sendingDate + "-" + receivingDate);
        

        if (receivingDate == null)
        {
            throw new ArgumentNullException(nameof(receivingDate), "Receiving date cannot be null.");
        }   
        
        TimeSpan timeInterval = receivingDate - sendingDate;
        
        Console.WriteLine("Calculating priority. Sending Date: {0}, Receiving Date: {1}, Interval (Days): {2}", 
            sendingDate, receivingDate, timeInterval.TotalDays);

        if (timeInterval.TotalDays <= 2)
        {
            return PriorityLevel.Urgent;
        }
        else if (timeInterval.TotalDays >= 2 && timeInterval.TotalDays <= 5)
        {
            return PriorityLevel.Standard;
        }
        else
        {
            return PriorityLevel.Low;
        }
    }
}