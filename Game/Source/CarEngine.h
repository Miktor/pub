#ifndef _CARENGINE_H_
#define _CARENGINE_H_

#ifndef _PRECOMP
#define _PRECOMP
#endif

#include "stdafx.h"

namespace RacingGame
{
	class CarEngine
	{
	public:
		CarEngine(btRaycastVehicle *_mVehicle);
		~CarEngine();

		void update(float deltat)				{ _computeAxisTorque(deltat);}

		btScalar getThrottle()				{ return Throttle;		}
		void	 setThrottle(btScalar th)	{ Throttle = th;		}

		btScalar getClutch()				{ return Clutch;		}
		void	 setClutch(btScalar cl)		{ Clutch = cl;			}

		btScalar getRPM()					{ return RPM;			}
		btScalar getEngineForce()			{ return EngineForce;	}
		int		 getGear()					{ return CurGear;		}

		void	 upGear()					{CurGear++;}
		void	 downGear()					{CurGear--;}
		unsigned int getCurGear()			{return CurGear;}

	protected:

		btScalar Clutch;
		btScalar Throttle;

		void _computeAxisTorque(float time);
		btScalar getTorque(btScalar _RPM);
		btScalar computeRpmFromWheels(float time);
		btScalar computeMotorRpm(btScalar rpm);
		int changeGears();

		btRaycastVehicle* mVehicle;
		btScalar RPM;
		float EngineForce;
		btScalar Efficiency;

		unsigned int CurGear;

		btScalar MainGear;
		btScalar Gears[8];
		btScalar IdleRPM;
		btScalar MaxRPM;

		btScalar Torque[10][2];
	};
}
#endif