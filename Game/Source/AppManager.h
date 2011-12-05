#ifndef AppManager_H_
#define AppManager_H_

#include "stdafx.h"

namespace RacingGame
{
	class AppInput;
	class MyCamera;
	class Object;

	class AppManager : protected Ogre::FrameListener, protected Ogre::WindowEventListener
	{
	private:
		bool _started;
	public:
		std::vector<AppState*>           _states;
		std::map<std::string, AppState*> _statesMap;

		AppManager() : mRoot(0), mRenderWindow(0), mSceneMgr(0),
			mGui(0), mPlatform(0),
			mInput(0), mCamera(0), mInfo(0),
			_started(0), count(0)
		{	}
		~AppManager();

		void start(AppState* firstState,AppManager* manager);
		void shutdown();

		void      addState(std::string name, AppState* inst);
		AppState* getState(std::string name);

		MyGUI::Gui* getGUI() { return mGui; }

		void changeState(AppState* state);
		void push(AppState* state);
		bool pop();

		AppState* getStateByName(std::string name)
		{
			return _statesMap[name];
		}
		AppState* getEndState()
		{
			return _states.back();
		}

		void addUpdObj(UpdatableObject* obj)
		{
			myUpdObjects.push_back(obj);
		}

		Ogre::Root* mRoot;
		Ogre::RenderWindow* mRenderWindow;
		Ogre::SceneManager* mSceneMgr;

		MyGUI::Gui* mGui;
		MyGUI::OgrePlatform* mPlatform;

		AppInput* mInput;
		MyCamera* mCamera;

		StatisticInfo* mInfo;

		std::vector<Object*> myObjects;
		std::vector<UpdatableObject*> myUpdObjects;

		size_t count;

	protected:
		void loadConfig(std::string);

		bool frameStarted(const Ogre::FrameEvent &evt);
		bool frameRenderingQueued(const Ogre::FrameEvent &evt);
		bool frameEnded(const Ogre::FrameEvent &evt);
		void windowResized(Ogre::RenderWindow* rw);
		void windowClosed(Ogre::RenderWindow* rw)	{ shutdown(); }
	};
}

#endif