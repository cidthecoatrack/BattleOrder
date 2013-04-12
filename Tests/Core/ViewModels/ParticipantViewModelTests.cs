using System.Linq;
using BattleOrder.Core.Models.Actions;
using BattleOrder.Core.Models.Participants;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Core.ViewModels
{
    [TestFixture]
    public class ParticipantViewModelTests
    {
        ActionParticipant participant;
        ParticipantViewModel participantViewModel;

        [SetUp]
        public void Setup()
        {
            participant = new ActionParticipant("name", false, false);
            participantViewModel = new ParticipantViewModel(participant);
        }

        [Test]
        public void CorrectLoad()
        {
            Assert.That(participantViewModel.Name, Is.EqualTo(participant.Name));
            Assert.That(participantViewModel.IsEnemy, Is.EqualTo(participant.IsEnemy), "IsEnemy");
            Assert.That(participantViewModel.IsNpc, Is.EqualTo(participant.IsNpc), "IsNPC");
        }

        [Test]
        public void CorrectSave()
        {
            participantViewModel.Name = "New Name";
            participantViewModel.IsNpc = true;

            participantViewModel.SaveChanges();

            Assert.That(participant.Name, Is.EqualTo(participantViewModel.Name));
            Assert.That(participantViewModel.IsNpc, Is.EqualTo(participantViewModel.IsNpc));
        }

        [Test]
        public void AddsAction()
        {
            var action = new BattleAction("attack name", 1, 1);
            var actionCount = participant.Actions.Count();
            participantViewModel.AddAction(action);

            Assert.That(participant.Actions, Contains.Item(action));
            Assert.That(participant.Actions.Count(), Is.EqualTo(actionCount + 1));
        }
    }
}