using System;
using System.Collections.Generic;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;
namespace BattleOrder.Tests.ViewModels
{
    [TestFixture]
    public class RoundViewModelTests
    {
        RoundViewModel roundViewModel;

        [SetUp]
        public void Setup()
        {
            var attack = new Attack("attack", 1, 1, true);
            var attacks = new Queue<QueueableAttack>();
            attacks.Enqueue(new QueueableAttack("Name", attack, 2));
            attacks.Enqueue(new QueueableAttack("Other Name", attack, 1));

            roundViewModel = new RoundViewModel(attacks, 1);
        }
        
        [Test]
        public void CorrectLoad()
        {
            Assert.That(roundViewModel.CompletedAttacks, Is.EqualTo(1));
            Assert.That(roundViewModel.CurrentAttacks, Is.EqualTo("\nName's attack"));
            Assert.That(roundViewModel.RoundIsActive, Is.True);
            Assert.That(roundViewModel.RoundTitle, Is.EqualTo("Round 1: -1"));
            Assert.That(roundViewModel.TotalAttacks, Is.EqualTo(2));
        }

        [Test]
        public void GetAttacks()
        {
            roundViewModel.GetAttacks();
            
            Assert.That(roundViewModel.CompletedAttacks, Is.EqualTo(2));
            Assert.That(roundViewModel.CurrentAttacks, Is.EqualTo("\nOther Name's attack"));
            Assert.That(roundViewModel.RoundIsActive, Is.False);
            Assert.That(roundViewModel.RoundTitle, Is.EqualTo("Round 1: 0"));
        }
    }
}