#ifndef AppInput_H_
#define AppInput_H_

#include "stdafx.h"

namespace RacingGame
{
	class AppManager;

	class AppInput : public OIS::MouseListener,	public OIS::KeyListener, public OIS::JoyStickListener
	{
	private:
		AppManager *mAppMngr;
		OIS::InputManager *m_ois;
		OIS::Mouse *mMouse;
		OIS::Keyboard *mKeyboard;
		unsigned long m_hWnd;
	public:
		AppInput(unsigned long hWnd, AppManager *_mAppMngr);
		~AppInput();

		void setWindowExtents(int width, int height) ;
		void capture();

		// MouseListener
		bool mouseMoved(const OIS::MouseEvent &evt);
		bool mousePressed(const OIS::MouseEvent &evt, OIS::MouseButtonID);
		bool mouseReleased(const OIS::MouseEvent &evt, OIS::MouseButtonID);

		// KeyListener
		bool keyPressed(const OIS::KeyEvent &evt);
		bool keyReleased(const OIS::KeyEvent &evt);

		// JoyStickListener
		bool buttonPressed(const OIS::JoyStickEvent &evt, int index);
		bool buttonReleased(const OIS::JoyStickEvent &evt, int index);
		bool axisMoved(const OIS::JoyStickEvent &evt, int index);
		bool povMoved(const OIS::JoyStickEvent &evt, int index);
	};
}
#endif