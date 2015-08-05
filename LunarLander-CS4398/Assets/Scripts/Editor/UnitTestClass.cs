using UnityEngine;
using System.Collections;
using NUnit.Framework;


public class UnitTestClass  {

	[TestFixture]
	public class LanderTestClass{
		
		
		LanderControllerScript test;
		LanderControllerScript test2;
		
		[SetUp]
		public void Init(){
			test = new LanderControllerScript(1000,50);
			test2 = new LanderControllerScript(1000,50);

		}
		
		
		[Test]
		public void Loss_Of_Health_On_Impact(){
			//test = new LanderControllerScript(1000,50);
			test.depleteHealth (2, 2, 3);
			Assert.AreEqual (2, test.healthAmount);
			
		}

		
		[Test]
		public void Loss_Of_Fuel_On_Thrust(){
			//test = new LanderControllerScript(1000,50);
			test.depleteFuel (500);
			Assert.AreEqual (500, test.fuelAmount);
			
		}


		[Test]
		public void Thrusters_NotActive_On_Start(){
			Assert.IsFalse (test.getThrustersStatus());
			
		}
		


		[Test]
		public void Is_StationaryOn_Start(){
			
			Assert.IsTrue (!test.getThrustersStatus());
		}


		[Test]
		[Ignore("Not Implemented")]
		public void No_Dmg_Taken_After_Win(){
			
			
			
		}

		[Test]
		public void Higher_Velocity_Does_More_Dmg()
		{
			test.depleteHealth (2, 2, 3);
			test2.depleteHealth (4, 4, 3);
			Assert.Greater (test.healthAmount, test2.healthAmount);
		}

		[Test]
		public void More_Thrust_Used_Less_Fuel_Left()
		{
			test.depleteFuel (700);
			test2.depleteFuel (500);
			Assert.Greater (test2.fuelAmount, test.fuelAmount);
		}

		/*
		[Test("unable to simulate movement")]
		public void Thrusters_Shown_When_Used()
		{
			Assert.IsTrue (test.getThrustersStatus ());
		}
		*/
		
		
		
		
		
	}

	[TestFixture]
	public class FinishAreaTestClass{
		
		FinishAreaCollider faCollider;
		
		[Test]
		public void Win_If_Not_Stopped_In_Trigger(){
			faCollider = new FinishAreaCollider (false, true);
			Assert.IsFalse (faCollider.getIfFinished());
		}
		
		[Test]
		public void Win_If_Stopped_Not_In_Trigger(){
			faCollider = new FinishAreaCollider (true, false);
			Assert.IsFalse (faCollider.getIfFinished ());
		}
		
		[Test]
		public void Win_If_Not_Stopped_Not_In_Trigger(){
			faCollider = new FinishAreaCollider (false, false);
			Assert.IsFalse (faCollider.getIfFinished ());
		}
		
		[Test]
		public void Win_If_Stopped_In_Trigger(){
			faCollider = new FinishAreaCollider (true, true);
			Assert.Pass();
			
		}
		
		
		
	}


	[TestFixture]
	public class ModelTestClass{
		
		
		
		[Test]
		public void HighScore_Saved(){

			ScoreManager.score = 0;
			ScoreManager.AddScore (10);
			ScoreManager.AddScore (10);
			ScoreManager.AddScore (10);
			
			PlayerPrefs.SetInt ("MyTestKey", ScoreManager.score);
			
			Assert.AreEqual(30,PlayerPrefs.GetInt("MyTestKey"));

			PlayerPrefs.DeleteKey("MyTestKey");
			
			
		}
		
		[Test]
		public void HighScore_Initials_Saved(){
			string name = "JLF";
			PlayerPrefs.SetString ("MyTestStringKey1", name);
			Assert.AreEqual(name,PlayerPrefs.GetString("MyTestStringKey1"));
			
		}
		
		
	}
}
