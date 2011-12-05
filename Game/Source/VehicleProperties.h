#ifndef VehicleProperties_H
#define VehicleProperties_H

#include "stdafx.h"
#include "SlidingPanel.h"
#include "GameState.h"
#include "AppManager.h"

namespace RacingGame
{
	class VehicleProperties : public AppState
	{
	public:
		VehicleProperties(AppManager *am);
		~VehicleProperties();

		void enter();
		void exit();

		void pause();
		void resume();

		bool frameStarted(const Ogre::FrameEvent &evt){ AppMgr->getGUI()->injectFrameEntered(evt.timeSinceLastFrame); return !StopScene;}
		bool frameRenderingQueued(const Ogre::FrameEvent &evt){ return !StopScene;}
		bool frameEnded(const Ogre::FrameEvent &evt){ return !StopScene;}

		bool mouseMoved(const OIS::MouseEvent &arg){ AppMgr->getGUI()->injectMouseMove(arg.state.X.abs, arg.state.Y.abs, arg.state.Z.abs); return !StopScene;}
		bool mousePressed(const OIS::MouseEvent &arg, OIS::MouseButtonID id){ AppMgr->getGUI()->injectMousePress(arg.state.X.abs, arg.state.Y.abs, MyGUI::MouseButton::Enum(id));	 return !StopScene;}
		bool mouseReleased(const OIS::MouseEvent &arg, OIS::MouseButtonID id){ AppMgr->getGUI()->injectMouseRelease(arg.state.X.abs, arg.state.Y.abs, MyGUI::MouseButton::Enum(id)); return !StopScene;}

		bool keyPressed(const OIS::KeyEvent &arg);
		bool keyReleased(const OIS::KeyEvent &arg){ AppMgr->getGUI()->injectKeyRelease(MyGUI::KeyCode::Enum(arg.key)); return !StopScene;}

		void setProperties(floatMap* opt);
	private:
		PanelViewWindow* mView;
		PanelDynamic*    mPanelDynamic;
		floatMap::iterator optionsBegin;
		floatMap::iterator optionsEnd;
		floatMap* pOpt;
	};
}

#endif