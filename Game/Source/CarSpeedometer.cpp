#include "CarSpeedometer.h"

namespace RacingGame
{
	VehicleSpeedometer::VehicleSpeedometer()
	{
	}

	VehicleSpeedometer::~VehicleSpeedometer()
	{
		delete mBackground;
		delete mArrow;
	}

	void VehicleSpeedometer::init(Ogre::Real x, Ogre::Real y)
	{
		MyGUI::LayoutManager::getInstance().loadLayout("Odometer.layout");

		//mPanel =	  MyGUI::Gui::getInstance().findWidget<MyGUI::Widget>	("odometer");
		mBackground = MyGUI::Gui::getInstance().findWidget<MyGUI::ImageBox>	("odometerBackground");
		mVelOut     = MyGUI::Gui::getInstance().findWidget<MyGUI::TextBox>	("VelOut");
		MyGUI::ImageBox* rotSkin = MyGUI::Gui::getInstance().findWidget<MyGUI::ImageBox>("odometerRotation");
		mArrow      = rotSkin->getSubWidgetMain()->castType<MyGUI::RotatingSkin>();

		mArrow->setCenter(MyGUI::IntPoint(Ogre::StringConverter::parseInt(rotSkin->getUserString("CenterX")),
			Ogre::StringConverter::parseInt(rotSkin->getUserString("CenterY"))));
		mBackground->setPosition(x, y);

		maxAngle = Ogre::StringConverter::parseReal(rotSkin->getUserString("MaxAngle")) * Ogre::Math::PI;
		addAngle = Ogre::StringConverter::parseReal(rotSkin->getUserString("AddAngle")) * Ogre::Math::PI;
		maxVel   = Ogre::StringConverter::parseReal(rotSkin->getUserString("MaxVel"));
		doubled =  Ogre::StringConverter::parseBool(rotSkin->getUserString("Doubled"));
	}

	void VehicleSpeedometer::update(float velosoty)
	{
		mVelOut->setCaption(Ogre::StringConverter::toString((int)velosoty) + " KM/H");

		float angle = maxAngle * velosoty / maxVel;
		if(velosoty > maxVel && doubled && velosoty < 2 * maxVel)
			angle -= maxAngle;
		else if(velosoty >= 2 * maxVel)
			angle += Ogre::Math::RangeRandom(-0.1, 0.1);

		mArrow->setAngle(angle + addAngle);
	}
}