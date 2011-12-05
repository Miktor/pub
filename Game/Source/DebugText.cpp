#include "DebugText.h"
#include "mCars.h"
#include "CarEngine.h"
#include "AppManager.h"
#include "DynamicLines.h"

using namespace Ogre;

DebugCarText::DebugCarText(AppManager* _appMngr)
{
	appMngr = _appMngr;
	track = 1;
	init();
}
DebugCarText::DebugCarText(mCar* car, AppManager* _appMngr)
{
	appMngr = _appMngr;
	trackingCar = car;
	track = 0;
	init();
}
DebugCarText::~DebugCarText()
{
	overlay->clear();
	overlay = NULL;
	trackingCar = NULL;
	textArea = NULL;
}

void DebugCarText::SetTrackingCar(mCar* car)
{
	trackingCar = car;
	track = 0;
}
void DebugCarText::update()
{
	//String text = "FPS = " + StringConverter::toString(appMngr->mRoot->getAutoCreatedWindow()->getLastFPS()) + "\r\n";
	//text += "Triangles = " + StringConverter::toString(appMngr->mRoot->getAutoCreatedWindow()->getTriangleCount()) + "\r\n";
	//text += "Throttle = " + StringConverter::toString(trackingCar->getEngine()->getThrottle()) + " | " + "Clutch = " + StringConverter::toString(trackingCar->getEngine()->getClutch()) + " | " + "Gear = " + StringConverter::toString(trackingCar->getEngine()->getGear()) + "\r\n";
	//text += "RPM = " + StringConverter::toString(trackingCar->getEngine()->getRPM()) + "\r\n";
	//text += "EngineForce = " + StringConverter::toString(trackingCar->getEngine()->getEngineForce()) + "\r\n";
	//text += "Velocity = " + StringConverter::toString(trackingCar->getSpeed()) + "\r\n";

	//textArea->setCaption(text);
}
void DebugCarText::init()
{
	panel = static_cast<OverlayContainer*>( OverlayManager::getSingletonPtr()->createOverlayElement("Panel", "PanelStats") );
	panel->setMetricsMode(Ogre::GMM_PIXELS);
	panel->setPosition(10, 10);
	panel->setDimensions(300, 120);

	textArea = static_cast<TextAreaOverlayElement*>( OverlayManager::getSingletonPtr()->createOverlayElement("TextArea", "TextAreaName") );
	textArea->setMetricsMode(Ogre::GMM_PIXELS);
	textArea->setPosition(0, 0);
	textArea->setDimensions(300, 120);
	textArea->setCharHeight(28);
	textArea->setFontName("MyFont");

	overlay = OverlayManager::getSingletonPtr()->create("OverlayStats");
	overlay->add2D(panel);

	panel->addChild(textArea);
	overlay->show();
}