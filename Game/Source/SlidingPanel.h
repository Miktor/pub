#ifndef SlidingPanel_H_
#define SlidingPanel_H_

#include <string.h>
#include "MyGUI.h"

#include "PanelView/BasePanelView.h"
#include "PanelView/BasePanelViewCell.h"
#include "PanelView/BasePanelViewItem.h"

namespace RacingGame
{
	class PanelViewCell : public wraps::BasePanelViewCell
	{
	public:
		PanelViewCell(MyGUI::Widget* _parent) : BasePanelViewCell("PanelCell.layout", _parent)
		{
			assignWidget(mTextCaption, "text_Caption");
			assignWidget(mButtonMinimize, "button_Minimize");
			assignWidget(mWidgetClient, "widget_Client");

			mTextCaption->eventMouseButtonDoubleClick += MyGUI::newDelegate(this, &PanelViewCell::notifyMouseButtonDoubleClick);
			mButtonMinimize->eventMouseButtonPressed += MyGUI::newDelegate(this, &PanelViewCell::notfyMouseButtonPressed);
		}

		virtual void setMinimized(bool _minimized)
		{
			wraps::BasePanelViewCell::setMinimized(_minimized);
			mButtonMinimize->setStateSelected(isMinimized());
		}

	private:
		void notfyMouseButtonPressed(MyGUI::Widget* _sender, int _left, int _top, MyGUI::MouseButton _id)
		{
			if (_id == MyGUI::MouseButton::Left)
			{
				setMinimized(!isMinimized());
			}
		}

		void notifyMouseButtonDoubleClick(MyGUI::Widget* _sender)
		{
			setMinimized(!isMinimized());
		}

	private:
		MyGUI::Button* mButtonMinimize;
	};

	class PanelView : public wraps::BasePanelView<PanelViewCell>
	{
		public:
			PanelView(MyGUI::Widget* _parent) :  wraps::BasePanelView<PanelViewCell>("", _parent)
			  {
			  }
	};

	class PanelViewWindow :	public wraps::BaseLayout
	{
	public:
		PanelViewWindow() : BaseLayout("PanelView.layout"), mPanelView(nullptr)
		{
			assignBase(mPanelView, "scroll_View");
		}

		PanelView* getPanelView()
		{
			return mPanelView;
		}

	private:
		PanelView* mPanelView;
	};

	class PanelDynamic : public wraps::BasePanelViewItem
	{
	public:
		PanelDynamic() : BasePanelViewItem("")
		{
		}

		virtual void initialise(std::string caption, std::vector<std::string> label)
		{
			mPanelCell->setCaption(caption);

			const int height = 24;
			const int height_step = 26;
			const int width = 200;
			const int width_step = 3;

			int height_current = 0;
			for (size_t pos = 0; pos < label.size(); ++pos)
			{
				MyGUI::TextBox* text = mWidgetClient->createWidget<MyGUI::TextBox>("TextBox", MyGUI::IntCoord(width_step, height_current, width, height), MyGUI::Align::Left | MyGUI::Align::Top);
				text->setTextAlign(MyGUI::Align::Right | MyGUI::Align::VCenter);
				text->setCaption(label[pos]);
				mItemsText.push_back(text);

				MyGUI::EditBox* edit = mWidgetClient->createWidget<MyGUI::EditBox>("Edit", MyGUI::IntCoord(width_step + width_step + width, height_current, mWidgetClient->getWidth() - (width_step + width_step + width_step + width), height), MyGUI::Align::HStretch | MyGUI::Align::Top);
				mItemsEdit.push_back(edit);

				height_current += height_step;
			}

			mPanelCell->setClientHeight(height_current, false);
		}
		virtual void shutdown()
		{
			mItemsText.clear();
			mItemsEdit.clear();
		}

		void setVisibleCount(size_t _count)
		{
			const int height = 24;
			const int width = 200;

			const int width_step = 3;
			const int height_step = 26;
			int height_current = 0;

			for (size_t pos = 0; pos < _count; ++pos)
			{
				if(pos < mItemsEdit.size())
				{
					mItemsText[pos]->setVisible(true);
					mItemsEdit[pos]->setVisible(true);
					height_current += height_step;
				}
				else
				{
					MyGUI::TextBox* text = mWidgetClient->createWidget<MyGUI::TextBox>("TextBox", MyGUI::IntCoord(width_step, height_current, width, height), MyGUI::Align::Left | MyGUI::Align::Top);
					text->setTextAlign(MyGUI::Align::Right | MyGUI::Align::VCenter);
					text->setCaption(MyGUI::utility::toString("label ", pos, ":"));
					mItemsText.push_back(text);

					MyGUI::EditBox* edit = mWidgetClient->createWidget<MyGUI::EditBox>("Edit", MyGUI::IntCoord(width_step + width_step + width, height_current, mWidgetClient->getWidth() - (width_step + width_step + width_step + width), height), MyGUI::Align::HStretch | MyGUI::Align::Top);
					mItemsEdit.push_back(edit);

					height_current += height_step;
				}
			}
			mPanelCell->setClientHeight(height_current, true);
		}

		MyGUI::VectorWidgetPtr mItemsText;
		MyGUI::VectorWidgetPtr mItemsEdit;
	};
}
#endif