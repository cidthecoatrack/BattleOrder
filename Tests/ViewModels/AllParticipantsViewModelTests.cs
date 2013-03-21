using System;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class AllParticipantsViewModelTests
    {
        private AllParticipantsViewModel allParticipantsViewModel;
        private Participant goodGuy;
        private Participant badGuy;
        
        [SetUp]
        public void Setup()
        {
            goodGuy = new Participant("good guy", isEnemy: false);
            badGuy = new Participant("bad guy");
            var participants = new[] { goodGuy, badGuy };

            goodGuy.AddAttack(new Attack(String.Empty));
            badGuy.AddAttack(new Attack(String.Empty));

            allParticipantsViewModel = new AllParticipantsViewModel(participants);
        }
        
        [Test]
        public void Constructor()
        {
            Assert.That(allParticipantsViewModel.PartyMembers.Count, Is.EqualTo(1));
            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(1));
            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(0));
        }

        [Test]
        public void SelectPartyMemberUpdatesAttacks()
        {
            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(1));
        }

        [Test]
        public void SelectEnemyUpdatesAttacks()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(1));
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
            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveBadGuyWhenSelected()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            allParticipantsViewModel.RemoveParticipant(badGuy);

            Assert.That(allParticipantsViewModel.Enemies.Count, Is.EqualTo(0));
            Assert.That(allParticipantsViewModel.CurrentEnemy, Is.Null);
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(0));
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
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddAttackToGoodGuy()
        {
            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            allParticipantsViewModel.AddAttackToPartyMember(new Attack("new attack"));

            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewGoodGuyAttackGenuinelySaved()
        {
            AddAttackToGoodGuy();

            allParticipantsViewModel.CurrentPartyMember = null;
            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(0));

            allParticipantsViewModel.CurrentPartyMember = goodGuy;
            Assert.That(allParticipantsViewModel.PartyMemberAttacks.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddAttackToBadGuy()
        {
            allParticipantsViewModel.CurrentEnemy = badGuy;
            allParticipantsViewModel.AddAttackToEnemy(new Attack("new attack"));

            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(2));
        }

        [Test]
        public void NewBadGuyAttackGenuinelySaved()
        {
            AddAttackToBadGuy();

            allParticipantsViewModel.CurrentEnemy = null;
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(0));

            allParticipantsViewModel.CurrentEnemy = badGuy;
            Assert.That(allParticipantsViewModel.EnemyAttacks.Count, Is.EqualTo(2));
        }
    }
}