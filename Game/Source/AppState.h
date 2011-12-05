#ifndef AppState_H
#define AppState_H

namespace RacingGame
{
	class AppManager;

	class AppState
	{
	protected:
		AppManager* AppMgr;
	public:

		bool StopScene;
		virtual void enter() = 0;
		virtual void exit() = 0;

		virtual void pause() = 0;
		virtual void resume() = 0;

		virtual bool frameStarted(const Ogre::FrameEvent &evt) = 0;
		virtual bool frameRenderingQueued(const Ogre::FrameEvent &evt) = 0;
		virtual bool frameEnded(const Ogre::FrameEvent &evt) = 0;

		virtual bool mouseMoved(const OIS::MouseEvent &arg) = 0;
		virtual bool mousePressed(const OIS::MouseEvent &arg, OIS::MouseButtonID id) = 0;
		virtual bool mouseReleased(const OIS::MouseEvent &arg, OIS::MouseButtonID id) = 0;

		virtual bool keyPressed(const OIS::KeyEvent &arg) = 0;
		virtual bool keyReleased(const OIS::KeyEvent &arg) = 0;
	};
}

#endif