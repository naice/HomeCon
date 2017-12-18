#include <Arduino.h>
#include <RCSwitch.h>
#define DEBUG 1

RCSwitch mySwitch = RCSwitch();

void Transmit(int pin, const char * sGroup, const char * sDevice, bool onOff)
{
  mySwitch.enableTransmit(pin);
  if (onOff)
  {
    mySwitch.switchOn(sGroup, sDevice);
  }
  else
  {
    mySwitch.switchOff(sGroup, sDevice);
  }
}

void RC433MhzController_handleI2CWrite(uint8_t package[], uint8_t packageLength)
{
	// Example package bytes:
	// 0 = CODE_1
	// 1 = CODE_2
	// 2 = CODE_3
	// 3 = CODE_4
	// 4 = CODE_5
  // 5 = CODE_A
  // 6 = CODE_B
  // 7 = CODE_C
  // 8 = CODE_D
  // 9 = CODE_E
  //10 = ACTIVE
	//11 = REPEAT_COUNT
	//12 = PIN
  if (packageLength != 13)
  {
#if DEBUG
  Serial.println("Received invalid 433MHz package.");
#endif
    return;
  }  
  char sGroup[6]; sGroup[5] = 0;
  char sDevice[6]; sDevice[5] = 0;
  for (int i = 0; i < 5; i++)
  {
    sGroup[i] = package[i] == 1 ? '1' : '0';
    sDevice[i] = package[i+5] == 1 ? '1' : '0';
  }  
  uint8_t active = package[10];
  uint8_t repeats = package[11];
  uint8_t pin = package[12];
  
#if DEBUG
  Serial.println("Sending 433MHz Switch=" + String(active) + " to Group=" + String(sGroup) + ", Device=" + String(sDevice));
#endif

  for (int i = 0; i < 3; i++)
  {
    Transmit(pin, sGroup, sDevice, active > 0);
    delay(100);  
  }

}
