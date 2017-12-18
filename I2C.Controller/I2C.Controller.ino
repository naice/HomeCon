#include <Wire.h>
#include "RC433MhzController.h"
#define DEBUG 1
#define DUMP_PACKAGE 0
#define I2C_SLAVE_ADDRESS 0x40
#define LED 13
#define I2C_ACTION_RC433MHz 2

void setup() {

	pinMode(LED, OUTPUT);
	digitalWrite(LED, HIGH);

#if DEBUG
	Serial.begin(9600);
  delay(2000);
	Serial.println("Opened serial port");
  delay(2000);
	Serial.flush();
#endif

  // open I2C on desired slave...
	Wire.begin(I2C_SLAVE_ADDRESS);
#if DEBUG
  Serial.println("I2C OPEN ON SLAVE ADDR: " + String(I2C_SLAVE_ADDRESS));
#endif
  // hook on receive
	Wire.onReceive(handleI2CWrite);
	digitalWrite(LED, LOW);
}

void loop() 
{
  // noop.
}

void handleI2CWrite(int dataLength)
{
	if (dataLength == 0) return;

#if DEBUG
	if (dataLength > 32) { 
		Serial.println(F("Received too large package"));
		return;
	}
  Serial.println("I2C WRITE received package with length " + String(dataLength));
#endif

  digitalWrite(LED, HIGH);
  // read action 
	uint8_t action = Wire.read();

#if DEBUG
  Serial.println("I2C WRITE for action " + String(action));
#endif
  // read package
	uint8_t package[32];
	size_t packageLength = dataLength - 1;
	Wire.readBytes(package, packageLength);
	
#if DUMP_PACKAGE
  String packageDump = "Package = ";
  for (int i = 0; i < packageLength; i++)
  {
    packageDump += String(i) + "=" + String(package[i]) + ", ";
  }
  Serial.println(packageDump);
#endif

	switch (action)
	{
   case I2C_ACTION_RC433MHz:
    {
      RC433MhzController_handleI2CWrite(package, packageLength);
      break;
    }
	}

	digitalWrite(LED, LOW);
}
