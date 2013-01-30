using System;
using BattleOrder.Commands;
using BattleOrder.Models.Attacks;
using BattleOrder.ViewModels;
using NUnit.Framework;

namespace BattleOrderTests.Commands
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
        public void CantExecuteWithInvalidPerRound()
        {
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.True);
            attackViewModel.DecrementPerRound();
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void CantExecuteWithInvalidAttackName()
        {
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.True);
            attackViewModel.AttackName = String.Empty;
            Assert.That(saveAttackEditsCommand.CanExecute(new Object()), Is.False);
        }

        [Test]
        public void ExecuteSavesAttack()
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