#include "AppInput.h"
#include "AppManager.h"

namespace RacingGame
{
	AppInput::AppInput(unsigned long hWnd, AppManager *_mAppMngr)
	{
		OIS::ParamList pl;
		pl.insert(OIS::ParamList::value_type("WINDOW", Ogre::StringConverter::toString(hWnd)));

		m_hWnd = hWnd;
		m_ois = OIS::InputManager::createInputSystem( pl );
		mMouse = static_cast<OIS::Mouse*>(m_ois->createInputObject( OIS::OISMouse, true ));
		mKeyboard = static_cast<OIS::Keyboard*>(m_ois->createInputObject( OIS::OISKeyboard, true));
		mMouse->setEventCallback(this);
		mKeyboard->setEventCallback(this);

		mAppMngr = _mAppMngr;
	}

	AppInput::~AppInput()
	{
		if (mMouse)
			m_ois->destroyInputObject(mMouse);
		if (mKeyboard)
			m_ois->destroyInputObject(mKeyboard);

		OIS::InputManager::destroyInputSystem(m_ois);
	}

	void AppInput::capture()
	{
		mMouse->capture();
		mKeyboard->capture();
	}

	void  AppInput::setWindowExtents(int width, int height){
		//Set Mouse Region.. if window resizes, we should alter this to reflect as well
		const OIS::MouseState &ms = mMouse->getMouseState();
		ms.width = width;
		ms.height = height;
	}

	bool AppInput::mouseMoved(const OIS::MouseEvent &evt)
	{
		return mAppMngr->getEndState()->mouseMoved(evt);
	}
	bool AppInput::mousePressed(const OIS::MouseEvent &evt, OIS::MouseButtonID id)
	{
		return mAppMngr->getEndState()->mousePressed(evt, id);
	}
	bool AppInput::mouseReleased(const OIS::MouseEvent &evt, OIS::MouseButtonID id)
	{
		return mAppMngr->getEndState()->mouseReleased(evt, id);
	}

	bool AppInput::keyPressed(const OIS::KeyEvent &evt)
	{
		return mAppMngr->getEndState()->keyPressed(evt);
	}
	bool AppInput::keyReleased(const OIS::KeyEvent &evt)
	{
		return mAppMngr->getEndState()->keyReleased(evt);
	}

	// JoyStickListener
	bool AppInput::buttonPressed(const OIS::JoyStickEvent &evt, int index) {
		return true;
	}

	bool AppInput::buttonReleased(const OIS::JoyStickEvent &evt, int index) {
		return true;
	}

	bool AppInput::axisMoved(const OIS::JoyStickEvent &evt, int index) {
		return true;
	}

	bool AppInput::povMoved(const OIS::JoyStickEvent &evt, int index) {
		return true;
	}
}