Create a dotnet 10 console application that can detect network outages.

When started, the application will start polling the address 8.8.8.8. The address will be pinged once per second. Ensure that it times out in less than a second (500ms) if it fails.
If the ping fails 3 times in a row, an outage should be registered in the console application. The start and end of the outage should be listed, along with the duration.

In addition, once per hour, it should print a status report stating the uptime % as well as the total seconds of downtime for that hour.

The console outputs should have timestamps so it is easy to understand when the outage occurred.