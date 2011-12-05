#include "VehicleProperties.h"

namespace RacingGame
{
	VehicleProperties::VehicleProperties(AppManager *am)
	{
		AppMgr = am;
		StopScene = false;
		mView = 0;
		mPanelDynamic = 0;
	}

	VehicleProperties::~VehicleProperties()
	{
	}

	void VehicleProperties::enter()
	{
		mView = new PanelViewWindow();
		mPanelDynamic = new PanelDynamic();

		mView->getPanelView()->addItem(mPanelDynamic);
	}
	void VehicleProperties::exit()
	{
		floatMap::iterator optIT;
		size_t i = 0;

		for(size_t i = 0; i < mPanelDynamic->mItemsText.size(); i++)
		{
			optIT = pOpt->find(static_cast<std::string>(static_cast<MyGUI::TextBox*>(mPanelDynamic->mItemsText[i])->getCaption()));
			optIT->second =  MyGUI::utility::parseFloat(static_cast<MyGUI::TextBox*>(mPanelDynamic->mItemsEdit[i])->getCaption());
		}
		mView->getPanelView()->removeAllItems();
		delete mView;
		mView = 0;
	}

	void VehicleProperties::pause()
	{
	}
	void VehicleProperties::resume()
	{
	}

	bool VehicleProperties::keyPressed(const OIS::KeyEvent &arg)
	{
		AppMgr->getGUI()->injectKeyPress(MyGUI::KeyCode::Enum(arg.key), arg.text);
		switch (arg.key)
		{
		case OIS::KC_ESCAPE:
			exit();
			AppMgr->_states.pop_back();
			break;
		}
		return !StopScene;
	}

	void VehicleProperties::setProperties(floatMap* opt)
	{
		size_t i = 0;
		pOpt = opt;

		if(!mPanelDynamic)
			return;

		mPanelDynamic->setVisibleCount(pOpt->size());

		for(floatMap::iterator curOpt = opt->begin(); curOpt != opt->end(); curOpt++)
		{
			static_cast<MyGUI::TextBox*>(mPanelDynamic->mItemsText[i])->setCaption(MyGUI::utility::toString(curOpt->first));
			static_cast<MyGUI::EditBox*>(mPanelDynamic->mItemsEdit[i])->setCaption(MyGUI::utility::toString(curOpt->second));
			i++;
		}
	}
}