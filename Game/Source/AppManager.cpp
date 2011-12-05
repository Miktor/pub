#include "AppManager.h"
#include "Camera.h"
#include "AppInput.h"

using namespace Ogre;

namespace RacingGame
{
	AppManager::~AppManager()
	{
		if (_started) {
			shutdown();
		}
	}
	void AppManager::start(AppState* state,AppManager* manager)
	{
		unsigned long hWnd;

		if (_started) { return; }

		mRoot = new Root();
		loadConfig("..\\resources.cfg");

		// попробуем завестись на дефолтных
		if (!mRoot->restoreConfig())
		{
			// ничего не получилось, покажем диалог
			if (!mRoot->showConfigDialog()) return;
		}

		mRenderWindow = mRoot->initialise(true, "Racing Game");
		mSceneMgr = mRoot->createSceneManager(ST_GENERIC, "sceneManager");

		mCamera = new MyCamera(mSceneMgr, mRenderWindow);
		mCamera->initCamera();

		//updatableWithReal.push_back(mCamera->update);		

		ResourceGroupManager::getSingleton().initialiseAllResourceGroups();


		mRenderWindow->getCustomAttribute("WINDOW", &hWnd);
		mInput = new AppInput(hWnd, this);
		mInput->setWindowExtents(mRenderWindow->getWidth(), mRenderWindow->getHeight());

		mPlatform = new MyGUI::OgrePlatform();
		mPlatform->initialise(mRenderWindow, mSceneMgr);
		mGui = new MyGUI::Gui();
		mGui->initialise();

		MyGUI::ResourceManager::getInstance().load("MyGUI_BlackOrangeTheme.xml");

		MyGUI::LayoutManager::getInstance().loadLayout("HelpPanel.layout");
		mInfo = new StatisticInfo();

		Ogre::WindowEventUtilities::addWindowEventListener(mRenderWindow,this);
		mRoot->addFrameListener(this);

		changeState(state);

		_started = true;

		try
		{
			//Profiler::getSingleton().beginProfile("MyProfile");
			mRoot->startRendering();
			shutdown();
		}
		catch (Exception* e)
		{
			Ogre::LogManager::getSingletonPtr()->getDefaultLog()->logMessage(e->getFullDescription());
		}
	}
	void AppManager::shutdown()
	{
		if(!_started)
			return;
		while(pop());

		if(mGui)
		{
			mGui->shutdown();
			delete mGui;
			mGui = 0;
			mPlatform->shutdown();
			delete mPlatform;
			mPlatform = 0;
		}

		if(mCamera)
			delete mCamera;
		if (mRenderWindow)
		{
			mRenderWindow->destroy();
			mRenderWindow = 0;
		}
		if (mSceneMgr)
		{
			mSceneMgr->clearScene();
			mRoot->destroySceneManager(mSceneMgr);
			mSceneMgr = 0;
		}

		if (mInput)
			delete mInput;

		_statesMap.clear();
		_states.clear();
		//if (mRoot)
		//{
		//	mRoot->getAutoCreatedWindow()->removeAllViewports();
		//	delete mRoot;
		//	//mRoot = 0;
		//}
		_started = false;
	}
	void AppManager::addState(std::string name, AppState* inst)
	{
		_statesMap[name] = inst;
	}
	AppState* AppManager::getState(std::string name)
	{
		return _statesMap[name];
	}
	void AppManager::changeState(AppState* state)
	{
		// cleanup the current state
		if (!_states.empty()) {
			_states.back()->exit();
			_states.pop_back();
		}

		// store and init the new state
		_states.push_back(state);
		_states.back()->enter();
	}
	void AppManager::push(AppState* state)
	{
		// pause current state
		if ( !_states.empty() ) {
			_states.back()->pause();
		}

		// store and init the new state
		_states.push_back(state);
		_states.back()->enter();
	}
	bool AppManager::pop()
	{
		if ( !_states.empty() )
		{
			_states.back()->exit();
			_states.pop_back();
			return true;
		}
		else
			return false;
	}
	void AppManager::loadConfig(std::string name)
	{
		ConfigFile cf;
		cf.load(name);

		ConfigFile::SectionIterator seci = cf.getSectionIterator();

		String secName, typeName, archName;
		while (seci.hasMoreElements())
		{
			secName = seci.peekNextKey();
			ConfigFile::SettingsMultiMap *settings = seci.getNext();
			ConfigFile::SettingsMultiMap::iterator i;
			for (i = settings->begin(); i != settings->end(); ++i)
			{
				typeName = i->first;
				archName = i->second;
				ResourceGroupManager::getSingleton().addResourceLocation(
					archName, typeName, secName);
			}
		}
	}
	bool AppManager::frameStarted(const Ogre::FrameEvent &evt)
	{
		if(!_started)
			return false;

		if(mInput)
			mInput->capture();

		bool rez = _states.back()->frameStarted(evt);

		for (size_t i = 0; i < myUpdObjects.size(); i++)
			myUpdObjects[i]->update(evt.timeSinceLastFrame);

		return _started;
	}

	bool AppManager::frameRenderingQueued(const Ogre::FrameEvent &evt)
	{
		return _started;
	}

	bool AppManager::frameEnded(const Ogre::FrameEvent &evt)
	{
		if (mRenderWindow->isClosed())
			return false;
		return _states.back()->frameEnded(evt);
	}

	void AppManager::windowResized(Ogre::RenderWindow* rw)
	{
		if(mInput)
			mInput->setWindowExtents(rw->getWidth(), rw->getHeight());
	}
}