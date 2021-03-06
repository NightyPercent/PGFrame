using UnityEngine;
using System;
using System.Collections;

namespace WS1
{

	using PGFrame;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using UniRx;

	public class GameCoreControllerBase<T> : ControllerBase<T>
		where T: Singleton<T>, new()
	{

		public override void Attach (ViewModelBase viewModel)
		{
			GameCoreViewModel vm = (GameCoreViewModel)viewModel;
			vm.RC_AddSomeBullet.Subscribe (_ => {
				AddSomeBullet ((GameCoreViewModel)viewModel);
			}).AddTo (viewModel.baseAttachDisposables);
			vm.RC_RemoveSomeBullet.Subscribe (_ => {
				RemoveSomeBullet ((GameCoreViewModel)viewModel);
			}).AddTo (viewModel.baseAttachDisposables);

			vm.FSM_GameState.Attach ();
			vm.FSM_GameState.CurrentState.Pairwise ().Subscribe (_ => {
				GameStateChanged ((GameCoreViewModel)viewModel, _.Current, _.Previous);
			});
		}

		public override void Detach (ViewModelBase viewModel)
		{
			base.Detach (viewModel);
		}

		
		/*  */
		public virtual void AddSomeBullet (GameCoreViewModel viewModel)
		{
		}
		/*  */
		public virtual void RemoveSomeBullet (GameCoreViewModel viewModel)
		{
		}
					
		public virtual void GameStateChanged (GameCoreViewModel viewModel, GameCoreFSM.State newState, GameCoreFSM.State oldState)
		{
		}
	}
}