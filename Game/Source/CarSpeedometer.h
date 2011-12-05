#ifndef VehicleSpeedometer_H_
#define VehicleSpeedometer_H_

#include "stdafx.h"

namespace RacingGame
{
	class VehicleSpeedometer
	{
	public:
		VehicleSpeedometer();
		~VehicleSpeedometer();

		void init(Ogre::Real x, Ogre::Real y);
		void update(float velosoty);
	private:
		float addAngle;
		float maxAngle;
		float maxVel;
		bool doubled;

		MyGUI::ImageBox* mBackground;
		MyGUI::TextBox* mVelOut;
		MyGUI::RotatingSkin* mArrow;
	};
}
#endif