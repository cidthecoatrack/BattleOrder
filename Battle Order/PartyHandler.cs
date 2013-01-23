using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BattleOrder.Attacks;

namespace BattleOrder
{
    public class PartyHandler
    {
        private List<Participant> party;

        public PartyHandler(IEnumerable<Participant> party)
        {
            this.party = party as List<Participant>;
        }
        
        public List<Participant> AddNewAttackToParty(Attack newAttack, String partyMemberName)
        {
            var goodGuyExists = party.Any(x => x.Name == partyMemberName);

            if (!goodGuyExists)
                AddNewPartyMember(newAttack, partyMemberName);
            else
                AddNewAttackToPartyMember(newAttack, partyMemberName);

            return party;
        }

        private void AddNewAttackToPartyMember(Attack newAttack, String partyMemberName)
        {
            var goodguy = party.First(x => x.Name == partyMemberName);
            var attackExists = goodguy.Attacks.Any(x => x.Name == newAttack.Name);

            if (!attackExists)
                AddNewAttack(newAttack, goodguy);
            else
                ModifyAttack(newAttack, goodguy);
        }

        private void ModifyAttack(Attack newAttack, Participant participant)
        {
            var attack = participant.Attacks.First(x => x.Name == newAttack.Name);

            if (attack.Equals(newAttack))
            {
                AlterCurrentAttacks(participant, attack);
                return;
            }

            var message = "The attack data does not match.  Did you mean to edit the attack data?";
            var result = MessageBox.Show(message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
                EditAttack(newAttack, participant, attack);
        }

        private void EditAttack(Attack newAttack, Participant participant, Attack attackToEdit)
        {
            participant.Attacks.Remove(attackToEdit);

            if (participant.CurrentAttacks.Any())
            {
                var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?", participant.Name);
                SwitchCurrentAttack(newAttack, participant, message);
            }
            else
            {
                newAttack.Prepped = true;
            }

            participant.AddAttack(newAttack);
        }

        private void SwitchCurrentAttack(Attack newAttack, Participant participant, String message)
        {
            message += String.Format("\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.", participant.Name, participant.CurrentAttacksToString(), newAttack.Name);
            message += String.Format("\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.", participant.Name, participant.CurrentAttacksToString(), newAttack.Name);
            message += String.Format("\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", participant.Name);

            var result = MessageBox.Show(message, "Attack already there", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                foreach (var currentAttack in participant.CurrentAttacks)
                    currentAttack.Prepped = false;

            if (result == DialogResult.No || result == DialogResult.Yes)
                newAttack.Prepped = true;
        }

        private void AlterCurrentAttacks(Participant participant, Attack attack)
        {
            if (attack.Prepped)
            {
                CheckAndMakeSoleCurrentAttack(participant, attack);
            }
            else
            {
                if (participant.CurrentAttacks.Any())
                {
                    var message = "This attack has already been entered.  Make it a current attack?";
                    SwitchCurrentAttack(attack, participant, message);
                }
                else
                {
                    var message = String.Format("This attack has already been entered.  Make it {0}'s current attack?", participant.Name);
                    var result = MessageBox.Show(message, "Prep as current attack?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                        attack.Prepped = true;
                }
            }
        }

        private void CheckAndMakeSoleCurrentAttack(Participant participant, Attack attack)
        {
            if (participant.CurrentAttacks.Count() == 1)
            {
                MessageBox.Show("This attack has been already entered, and is prepped as the current attack.  You're being of the redundant, dufus.", "Error, dufus.");
                return;
            }

            var message = "This attack has already been entered and is prepped as a current attack.  Make it the only current attack?";
            var result = MessageBox.Show(message, "Switch?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                foreach (var currentAttack in participant.CurrentAttacks)
                    currentAttack.Prepped = false;
                attack.Prepped = true;
            }
        }

        private void AddNewAttack(Attack newAttack, Participant participant)
        {
            if (participant.CurrentAttacks.Any())
            {
                var message = String.Format("{0} already has an attack prepared.  Switch attack to new attack?\n\nPress \"Yes\" to switch {0}'s current attack from {1} to {2}.\nPress \"No\" to add {2} to {0}'s attacks, in addition to {1}.\nPress \"Cancel\" to not add the attack to {0}'s current attacks.", participant.Name, participant.CurrentAttacksToString(), newAttack.Name);
                var result = MessageBox.Show(message, "Attack already there", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    foreach (var attack in participant.Attacks)
                        attack.Prepped = false;

                if (result == DialogResult.No || result == DialogResult.Yes)
                    newAttack.Prepped = true;
            }
            else
            {
                newAttack.Prepped = true;
            }

            participant.AddAttack(newAttack);
        }

        private void AddNewPartyMember(Attack newAttack, String partyMemberName)
        {
            var result = MessageBox.Show("Is this character an NPC?", "NPC?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            var npc = (result != DialogResult.No);
            var newPerson = new Participant(partyMemberName, npc, newAttack);

            party.Add(newPerson);
        }
    }
}