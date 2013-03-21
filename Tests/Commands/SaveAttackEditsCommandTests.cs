using System;
using BattleOrder.Core.Commands;
using BattleOrder.Core.Models.Attacks;
using BattleOrder.Core.ViewModels;
using NUnit.Framework;

namespace BattleOrder.Tests.Commands
{
    [TestFixture]
    public class SaveAttackEditsCommandTests
    {
        Attack attack;
        AttackViewModel attackViewModel;
        SaveAttackEditsCommand saveAttackEditsCommand;
        
        [SetUp]
        public void Setup()
        {
            attack = new Attack("name", .5, 1);
            attackViewModel = new AttackViewModel(attack);
            saveAttackEditsCommand = new SaveAttackEditsCommand(attackViewModel);
        }
        
        [Test]
        public void InvalidPerRound()
        {
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.True);
            attackViewModel.DecrementPerRound();
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void InvalidAttackName()
        {
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.True);
            attackViewModel.AttackName = String.Empty;
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void Execute()
        {
            attackViewModel.AttackName = "New Name";
            attackViewModel.IncrementPerRound();
            attackViewModel.IncrementSpeed();
            
            saveAttackEditsCommand.Execute(new Object());
            Assert.That(attack.Name, Is.EqualTo("New Name"));
            Assert.That(attack.PerRound, Is.EqualTo(1));
            Assert.That(attack.Speed, Is.EqualTo(2));
        }
    }
}