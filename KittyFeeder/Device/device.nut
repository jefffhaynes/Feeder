
 
_drive <- hardware.pin7
 
_drive.configure(DIGITAL_OUT);

_driveOn <- false

_schedule <- {}

function feed(servings)
{
   server.log("feed " + servings + " servings");
  
   for(local i = 0; i < servings; i += 1)
   {
       runOnce();
   }
}

function setSchedule(schedule)
{
   server.log("set schedule: " + schedule);
   _schedule = schedule;
}

function runOnce()
{
    _driveOn = true;
    imp.wakeup(3.0, stop);
}

function stop()
{
    _driveOn = false;
}
 
function loop()
{
    if(_driveOn)
        _drive.write(1);
    else _drive.write(0);
    
    imp.wakeup(1.0, loop)
}

loop();
 
agent.on("feed", feed);
agent.on("schedule", setSchedule);

agent.on("feed", feed);