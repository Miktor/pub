//#include "MainMenu.h"
//#include "AppManager.h"
//
//
//MainMenu::MainMenu(AppManager* am)
//{
//	_am = am;
//}
//MainMenu::~MainMenu()
//{
//	_am = NULL;
//}
//void MainMenu::enter()
//{
//	MyGUI::LayoutManager::getInstancePtr()->load("MainMenu.layout");
//	MyGUI::ButtonPtr button = _am->mGui->findWidget<MyGUI::Button>("NewGame");
//	button->eventMouseButtonClick = MyGUI::newDelegate(this,&MainMenu::_changeMainMenuState);
//
//}
//void MainMenu::exit()
//{
//
//}
//void MainMenu::pause()
//{
//}
//void MainMenu::resume()
//{
//}
//void MainMenu::_changeMainMenuState(/*MyGUI::WidgetPtr _sender*/)
//{
//	_am->changeState(_am->getState("GameState"));
//}
//
//bool MainMenu::frameStarted(const Ogre::FrameEvent &evt)
//{
//	_am->mGui->injectFrameEntered(evt.timeSinceLastFrame);
//	return true;
//}
//bool MainMenu::frameRenderingQueued(const Ogre::FrameEvent &evt)
//{
//	return true;
//}
//bool MainMenu::frameEnded(const Ogre::FrameEvent &evt)
//{
//	return true;
//}
//
//bool MainMenu::mouseMoved(const OIS::MouseEvent &arg)
//{
//	_am->mGui->injectMouseMove(arg);
//	return true;
//}
//bool MainMenu::mousePressed(const OIS::MouseEvent &arg, OIS::MouseButtonID id)
//{
//	_am->mGui->injectMousePress(arg,id);
//	return true;
//}
//bool MainMenu::mouseReleased(const OIS::MouseEvent &arg, OIS::MouseButtonID id)
//{
//	_am->mGui->injectMouseRelease(arg,id);
//	return true;
//}
//
//bool MainMenu::keyPressed(const OIS::KeyEvent &arg)
//{
//	_am->mGui->injectKeyPress(arg);
//	if(arg.key==OIS::KC_ESCAPE)
//		delete _am;
//	return true;
//}
//bool MainMenu::keyReleased(const OIS::KeyEvent &arg)
//{
//	_am->mGui->injectKeyRelease(arg);
//	return true;
//}