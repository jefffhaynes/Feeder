// Log the URLs we need
server.log("Turn motor on: " + http.agenturl() + "?motor=1");
//server.log("Turn motor off: " + http.agenturl() + "?motor=0");
 
 
function requestHandler(request, response) {
  try {
    local path = request.path.tolower();
    
    if(path == "/feed")
        device.send("motor", 1); 
    else if(path == "/schedule")
    {
        server.log(request.body);
        var schedule = http.jsondecode(request.body);
        
    }
    // send a response back saying everything was OK.
    response.send(200, "OK");
  } catch (ex) {
    response.send(500, "Internal Server Error: " + ex);
  }
}
 
// register the HTTP handler
http.onrequest(requestHandler);