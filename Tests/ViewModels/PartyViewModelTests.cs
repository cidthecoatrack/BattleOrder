using System;
using System.Linq;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class PartyViewModelTests
    {
        private PartyViewModel partyViewModel;
        private ActionParticipant enabledGoodGuy;
        private ActionParticipant enabledBadGuy;
        private BattleAction goodAction;
        private BattleAction badAction;
        
        [SetUp]
        public void Setup()
        {
            enabledGoodGuy = new ActionParticipant("good guy", isEnemy: false);
            enabledBadGuy = new ActionParticipant("bad guy");
            var disabledGoodGuy = new ActionParticipant("good guy", isEnemy: false);
            disabledGoodGuy.Enabled = false;
            var disabledBadGuy = new ActionParticipant("bad guy");
            disabledBadGuy.Enabled = false;
            
            var participants = new[] 
            { 
                enabledGoodGuy, 
                enabledBadGuy,
                disabledGoodGuy,
                disabledBadGuy
            };

            goodAction = new BattleAction("Good");
            badAction = new BattleAction("Bad");

            enabledGoodGuy.AddAction(goodAction);
            enabledBadGuy.AddAction(badAction);

            partyViewModel = new PartyViewModel(participants);
        }
        
        [Test]
        public void Constructor()
        {
            Assert.That(partyViewModel.Party.Count, Is.EqualTo(2));
            Assert.That(partyViewModel.Enemies.Count, Is.EqualTo(2));
            Assert.That(partyViewModel.PartyMemberActions.Any(), Is.False);
            Assert.That(partyViewModel.EnemyActions.Any(), Is.False);
        }

        [Test]
        public void SelectPartyMemberUpdatesActions()
        {
            var count = enabledGoodGuy.Actions.Count;

            partyViewModel.CurrentPartyMember = enabledGoodGuy;
            Assert.That(partyViewModel.PartyMemberActions.Count, Is.EqualTo(count));
        }

        [Test]
        public void SelectEnemyUpdatesActions()
        {
            var count = enabledBadGuy.Actions.Count;

            partyViewModel.CurrentEnemy = enabledBadGuy;
            Assert.That(partyViewModel.EnemyActions.Count, Is.EqualTo(count));
        }

        [Test]
        public void AddGoodGuy()
        {
            var count = partyViewModel.Party.Count;

            partyViewModel.AddParticipant(new ActionParticipant("other good guy", isEnemy: false));
            Assert.That(partyViewModel.Party.Count, Is.EqualTo(count + 1));
        }

        [Test]
        public void AddBadGuy()
        {
            var count = partyViewModel.Enemies.Count;

            partyViewModel.AddParticipant(new ActionParticipant("other bad guy", isEnemy: true));
            Assert.That(partyViewModel.Enemies.Count, Is.EqualTo(count + 1));
        }

        [Test]
        public void RemoveGoodGuy()
        {
            var count = partyViewModel.Party.Count;

            partyViewModel.RemoveParticipant(enabledGoodGuy);
            Assert.That(partyViewModel.Party.Count, Is.EqualTo(count - 1));
        }

        [Test]
        public void RemoveBadGuy()
        {
            var count = partyViewModel.Enemies.Count;

            partyViewModel.RemoveParticipant(enabledBadGuy);
            Assert.That(partyViewModel.Enemies.Count, Is.EqualTo(count - 1));
        }

        [Test]
        public void RemoveGoodGuyWhenSelected()
        {
            var count = partyViewModel.Party.Count;

            partyViewModel.CurrentPartyMember = enabledGoodGuy;
            partyViewModel.RemoveParticipant(enabledGoodGuy);

            Assert.That(partyViewModel.Party.Count, Is.EqualTo(count - 1));
            Assert.That(partyViewModel.CurrentPartyMember, Is.Null);
            Assert.That(partyViewModel.PartyMemberActions.Any(), Is.False);
        }

        [Test]
        public void RemoveBadGuyWhenSelected()
        {
            var count = partyViewModel.Enemies.Count;

            partyViewModel.CurrentEnemy = enabledBadGuy;
            partyViewModel.RemoveParticipant(enabledBadGuy);

            Assert.That(partyViewModel.Enemies.Count, Is.EqualTo(count - 1));
            Assert.That(partyViewModel.CurrentEnemy, Is.Null);
            Assert.That(partyViewModel.EnemyActions.Any(), Is.False);
        }

        [Test]
        public void RemoveAllEnemies()
        {
            partyViewModel.RemoveAllEnemies();
            Assert.That(partyViewModel.Enemies.Any(), Is.False);
        }

        [Test]
        public void RemoveAllEnemiesWithSelectedEnemy()
        {
            partyViewModel.CurrentEnemy = enabledBadGuy;
            partyViewModel.RemoveAllEnemies();

            Assert.That(partyViewModel.Enemies.Any(), Is.False);
            Assert.That(partyViewModel.CurrentEnemy, Is.Null);
            Assert.That(partyViewModel.EnemyActions.Any(), Is.False);
        }

        [Test]
        public void AddActionToGoodGuy()
        {
            var count = enabledGoodGuy.Actions.Count;

            partyViewModel.CurrentPartyMember = enabledGoodGuy;
            partyViewModel.AddActionToPartyMember(new BattleAction("new attack"));

            Assert.That(partyViewModel.PartyMemberActions.Count, Is.EqualTo(count + 1));
        }

        [Test]
        public void NewGoodGuyActionGenuinelySaved()
        {
            AddActionToGoodGuy();

            var count = enabledGoodGuy.Actions.Count;

            partyViewModel.CurrentPartyMember = null;
            Assert.That(partyViewModel.PartyMemberActions.Any(), Is.False);

            partyViewModel.CurrentPartyMember = enabledGoodGuy;
            Assert.That(partyViewModel.PartyMemberActions.Count, Is.EqualTo(count));
        }

        [Test]
        public void AddActionToBadGuy()
        {
            var count = enabledBadGuy.Actions.Count;

            partyViewModel.CurrentEnemy = enabledBadGuy;
            partyViewModel.AddActionToEnemy(new BattleAction("new attack"));

            Assert.That(partyViewModel.EnemyActions.Count, Is.EqualTo(count + 1));
        }

        [Test]
        public void NewBadGuyActionGenuinelySaved()
        {
            AddActionToBadGuy();

            var count = enabledBadGuy.Actions.Count;

            partyViewModel.CurrentEnemy = null;
            Assert.That(partyViewModel.EnemyActions.Any(), Is.False);

            partyViewModel.CurrentEnemy = enabledBadGuy;
            Assert.That(partyViewModel.EnemyActions.Count, Is.EqualTo(count));
        }

        [Test]
        public void RemovePartyMemberAction()
        {
            AddActionToGoodGuy();

            var count = enabledGoodGuy.Actions.Count;
            partyViewModel.CurrentPartyMemberAction = goodAction;

            partyViewModel.RemovePartyMemberAction();

            Assert.That(partyViewModel.PartyMemberActions.Count, Is.EqualTo(count - 1));
            Assert.That(partyViewModel.CurrentPartyMemberAction, Is.Null);
        }

        [Test]
        public void RemoveEnemyAction()
        {
            AddActionToBadGuy();

            var count = enabledBadGuy.Actions.Count;
            partyViewModel.CurrentEnemyAction = badAction;

            partyViewModel.RemoveEnemyAction();

            Assert.That(partyViewModel.EnemyActions.Count, Is.EqualTo(count - 1));
            Assert.That(partyViewModel.CurrentEnemyAction, Is.Null);
        }

        [Test]
        public void GetEnabledParticipants()
        {
            var enabledParticipants = partyViewModel.GetEnabledParticipants();

            Assert.That(enabledParticipants, Contains.Item(enabledGoodGuy));
            Assert.That(enabledParticipants, Contains.Item(enabledBadGuy));
            Assert.That(enabledParticipants.Count(), Is.EqualTo(2));
        }
    }
}