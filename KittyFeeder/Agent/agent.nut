// Log the URLs we need
server.log("Feed: " + http.agenturl() + "/feed");
server.log("Schedule: " + http.agenturl() + "/schedule");
 
function requestHandler(request, response) 
{
  try 
  {
    local path = request.path.tolower();
    
    if(path == "/feed")
    {
        device.send("feed", 1); 
        response.send(200, "OK");
    }
    else if(path == "/schedule")
    {
        local stored = server.load();
        
        server.log(request.method);
        
        if(request.method == "POST")
        {
            server.log(request.body);
            local schedule = http.jsondecode(request.body);
            
            persistent <- {};
            
            if(stored.len() != 0)
                persistent = stored;
                
            persistent.schedule <- schedule;
            
            server.save(persistent);
            updateDevice();
            response.send(200, "OK");
        } 
        else if(request.method == "GET")
        {
            schedule <- {};
            
            if(stored.len() != 0)
                schedule = stored.schedule;
                
            local responseBody = http.jsonencode(schedule);
            response.send(200, responseBody);
        }
    }
  } 
  catch (ex) 
  {        
    response.send(500, "Internal Server Error: " + ex);
  }
}

function updateDevice()
{
    local persistent = server.load();
    
    if(persistent.len() != 0)
        device.send("state", persistent);
}

function onDeviceStatus(status)
{
    server.log("feed @ " + status.lastFeed);
    
    persistent <- {};
    
    local stored = server.load();
    if(stored.len() != 0)
        persistent = stored;
            
    persistent.lastFeed <- status.lastFeed;
        
    server.save(persistent);
    
    updateDevice();
}
 
device.onconnect(updateDevice);

device.on("status", onDeviceStatus)

// register the HTTP handler
http.onrequest(requestHandler);



