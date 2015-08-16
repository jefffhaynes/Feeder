
// These constants may be different for your servo
 
const SERVO_MIN = 0.060
//center = 0.075
const SERVO_MAX = 0.092

const OVERCURRENT_THRESHOLD = 53000

const SERVO_FORWARD = 0
const SERVO_BACKWARD = 1
const SERVO_STOP = 0.5


// Create global variable for the pin to which the servo is connected
// then configure the pin for PWM
 
servo <- hardware.pin7
servo.configure(PWM_OUT, 0.02, SERVO_MIN)
 
// Alias pin 2 as pot
 
servoFeedback <- hardware.pin1
 
// Configure the pin for analog input
 
servoFeedback.configure(ANALOG_IN)
 
// Define a function to control the servo.
// It expects a value between 0.0 and 1.0 to be passed to it
 
function setServo(value) 
{
    local scaledValue = value * (SERVO_MAX - SERVO_MIN) + SERVO_MIN
    servo.write(scaledValue)
    //server.log(scaledValue)
}
 


motorOn <- false
// Define a function to loop through the servo position values
 
function sweep() 
{
    if(motorOn)
        setServo(SERVO_FORWARD)
    else setServo(SERVO_STOP)
  
    local feedback = servoFeedback.read();
    //server.log(feedback)
    
    if(feedback > OVERCURRENT_THRESHOLD)
    {
        setServo(SERVO_BACKWARD)
        imp.sleep(0.1)
        setServo(SERVO_FORWARD)
    }
    
    imp.wakeup(1.0, sweep)
    
    // check time
    //local now = date();
    
    //if(now.sec==0)
    //    runOnce()
}
 
// Start the ball rolling by calling the loop function
 
    //sweep()
setServo(SERVO_STOP)

function setMotor(motorState) {
  server.log("Set motor: " + motorState);
  
  if(motorState == 1)
  {
     runOnce()
  }
  else motorOn = false;
}

function runOnce()
{
     motorOn = true;
     imp.wakeup(3.0, stop)
}

function stop()
{
    motorOn = false;
}
 
sweep();
     
// register a handler for "led" messages from the agent
agent.on("motor", setMotor);