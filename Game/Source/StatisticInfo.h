#ifndef __STATISTIC_INFO_H__
#define __STATISTIC_INFO_H__

#include "stdafx.h"

namespace RacingGame
{
	class StatisticInfo
	{
	public:
		typedef std::pair<std::string, std::string> PairString;
		typedef std::vector<PairString> VectorPairString;

		StatisticInfo() :
		mInfo(nullptr),
			mInfoShadow(nullptr)
		{
			const std::string layer = "Statistic";
			if (!MyGUI::LayerManager::getInstance().isExist(layer))
				return;

			mInfoShadow = MyGUI::Gui::getInstance().createWidget<MyGUI::TextBox>("TextBox", MyGUI::IntCoord(), MyGUI::Align::Default, layer);
			mInfoShadow->setTextColour(MyGUI::Colour::Black);
			mInfo = MyGUI::Gui::getInstance().createWidget<MyGUI::TextBox>("TextBox", MyGUI::IntCoord(), MyGUI::Align::Default, layer);
			mInfo->setTextColour(MyGUI::Colour::White);
		}

		~StatisticInfo()
		{
			if (mInfo != nullptr)
			{
				MyGUI::Gui::getInstance().destroyChildWidget(mInfo);
				mInfo = nullptr;
			}
			if (mInfoShadow != nullptr)
			{
				MyGUI::Gui::getInstance().destroyChildWidget(mInfoShadow);
				mInfoShadow = nullptr;
			}
		}

		template <typename T>
		void change(const std::string& _key, const T& _value)
		{
			for (VectorPairString::iterator iter = mParams.begin(); iter != mParams.end(); ++iter)
			{
				if (iter->first == _key)
				{
					iter->second = MyGUI::utility::toString<T>(_value);
					return;
				}
			}
			mParams.push_back(std::make_pair(_key, MyGUI::utility::toString<T>(_value)));
		}

		void remove(const std::string& _key)
		{
			for (VectorPairString::iterator iter = mParams.begin(); iter != mParams.end(); ++iter)
			{
				if (iter->first == _key)
				{
					mParams.erase(iter);
					return;
				}
			}
		}

		void update()
		{
			if (mInfo != nullptr)
			{
				std::ostringstream stream;
				for (VectorPairString::iterator iter = mParams.begin(); iter != mParams.end(); ++iter)
				{
					if (iter != mParams.begin()) stream << "\n";
					stream << iter->first << " : " << iter->second;
				}

				mInfo->setCaption(stream.str());
				mInfoShadow->setCaption(stream.str());

				MyGUI::ISubWidgetText* text = mInfo->getSubWidgetText();
				if (text != nullptr)
				{
					const MyGUI::IntSize& size = text->getTextSize() + mInfo->getSize() - text->getSize();
					const MyGUI::IntSize& size_view = MyGUI::RenderManager::getInstance().getViewSize();
					mInfo->setCoord(20, 20, size.width, size.height);
					if (mInfoShadow != nullptr)
						mInfoShadow->setCoord(21, 21, size.width, size.height);
				}
			}
		}

		void clear()
		{
			mParams.clear();
		}

		void clear(const std::string& _key)
		{
			for (VectorPairString::iterator iter = mParams.begin(); iter != mParams.end(); ++iter)
			{
				if (iter->first == _key)
				{
					mParams.erase(iter);
					return;
				}
			}
		}

		void setVisible(bool _value)
		{
			if (mInfo != nullptr)
				mInfo->setVisible(_value);
			if (mInfoShadow != nullptr)
				mInfoShadow->setVisible(_value);
		}

		bool getVisible()
		{
			if (mInfo != nullptr)
				return mInfo->getVisible();
			return false;
		}

	private:
		MyGUI::TextBox* mInfo;
		MyGUI::TextBox* mInfoShadow;
		VectorPairString mParams;
	};
}
#endif