#ifndef _DEBUGCARTEXT_H_
#define _DEBUGCARTEXT_H_

#include "stdafx.h"
#include "OgreTextAreaOverlayElement.h"

class mCar;
class AppManager;

class DebugCarText
{
public:
	DebugCarText(AppManager* _appMngr);
	DebugCarText(mCar* car, AppManager* _appMngr);
	~DebugCarText();
	void SetTrackingCar(mCar* car);
	void update();
protected:

	void init();
	AppManager* appMngr;
	Ogre::Overlay* overlay;
	Ogre::OverlayContainer* panel;
	Ogre::TextAreaOverlayElement* textArea;
	mCar* trackingCar;
	int track;
};

#endif