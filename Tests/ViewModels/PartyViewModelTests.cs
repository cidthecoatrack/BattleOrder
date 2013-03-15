using System;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class PartyViewModelTests
    {
        private PartyViewModel allParticipantsViewModel;
        private Participant goodGuy;
        private Participant badGuy;
        
        [SetUp]
        public void Setup()
        {
            goodGuy = new Participant("good guy", isEnemy: false);
            badGuy = new Participant("bad guy");
            var participants = new[] { goodGuy, badGuy };

            goodGuy.AddAction(new BattleAction(String.Empty));
            badGuy.AddAction(new BattleAction(String.Empty));

            allParticipantsViewModel = new PartyViewModel(participants);
        }
        
        [Test]
        public void Constructor()
        {
            Assert.That(allParticipantsViewModel.PartyMembers.Count, Is.EqualTo(1));
            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(1));
            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(0));
        }

        [Test]
        public void SelectPartyMemberUpdatesActions()
        {
            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(1));
        }

        [Test]
        public void SelectEnemyUpdatesActions()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddGoodGuy()
        {
            allParticipantsViewModel.AddParticipant(new Participant("other good guy", isEnemy: false));
            Assert.That(allParticipantsViewModel.PartyMembers.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddBadGuy()
        {

            allParticipantsViewModel.AddParticipant(new Participant("other bad guy", isEnemy: true));
            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveGoodGuy()
        {
            allParticipantsViewModel.RemoveParticipant(goodGuy);
            Assert.That(allParticipantsViewModel.PartyMembers.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveBadGuy()
        {
            allParticipantsViewModel.RemoveParticipant(badGuy);
            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveGoodGuyWhenSelected()
        {
            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            allParticipantsViewModel.RemoveParticipant(goodGuy);

            Assert.That(allParticipantsViewModel.PartyMembers.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.CurrentPartyMember, Is.Null);
            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveBadGuyWhenSelected()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            allParticipantsViewModel.RemoveParticipant(badGuy);

            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.CurrentEnemy, Is.Null);
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAllEnemies()
        {
            allParticipantsViewModel.RemoveAllEnemies();
            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAllEnemiesWithSelectedEnemy()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            allParticipantsViewModel.RemoveAllEnemies();

            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.CurrentEnemy, Is.Null);
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddActionToGoodGuy()
        {
            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            allParticipantsViewModel.AddActionToPartyMember(new BattleAction("new attack"));

            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewGoodGuyActionGenuinelySaved()
        {
            AddActionToGoodGuy();

            allParticipantsViewModel.CurrentPartyMember = null;
            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(0));

            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            Assert.That(allParticipantsViewModel.PartyMemberActions.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddActionToBadGuy()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            allParticipantsViewModel.AddActionToEnemy(new BattleAction("new attack"));

            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewBadGuyActionGenuinelySaved()
        {
            AddActionToBadGuy();

            allParticipantsViewModel.CurrentEnemy = null;
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(0));

            allParticipantsViewModel.CurrentEnemy = badGuy;
            Assert.That(allParticipantsViewModel.EnemyActions.Count, Is.EqualTo(2));
        }
    }
}