﻿using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root.Fx;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class PlantShape {
        public static void AddPlantShape() {

            var PlantShapeIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeI.jpg");
            var PlantShapeIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeII.jpg");
            var PlantShapeIIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeIII.jpg");
            var Icon_ScrollOfPlantShapeI = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeI.png");
            var Icon_ScrollOfPlantShapeII = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeII.png");
            var Icon_ScrollOfPlantShapeIII = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeIII.png");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var BiteLarge1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("ec35ef997ed5a984280e1a6d87ae80a8");
            var SlamLarge1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("7fe0fa95a5c21ee439e6849b7e018a82");
            var SlamGargantuan2n6 = Resources.GetBlueprint<BlueprintItemWeapon>("27eee74857c42db499b3a6b20cfa6211");
            var Slam1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("7445b0b255796d34495a8bca81b2e2d4");
            var TurnBarkStandart = Resources.GetBlueprint<BlueprintAbility>("bd09b025ee2a82f46afab922c4decca9");
            var MandragoraPoisonFeature = Resources.GetBlueprint<BlueprintFeature>("ec44af8b3449c5b4889145dbfc246a00");
            var DRSlashing10 = Resources.GetBlueprint<BlueprintFeature>("0df8cdae87d2a3047ad2b1c0568407e9");
            var FireVulnerability = Resources.GetBlueprint<BlueprintFeature>("8e934134fec60ab4c8972c85a7b62f89");
            var AcidResistance20 = Resources.GetBlueprint<BlueprintFeature>("416386972c8de2e42953533c4946599a");
            var TripImmunity = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var BlindSight = Resources.GetBlueprint<BlueprintFeature>("236ec7f226d3d784884f066aa4be1570");
            var OverrunAbility = Resources.GetBlueprint<BlueprintAbility>("1a3b471ecea51f7439a946b23577fd70");
            var BeastShapeIBuffPolymorph = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponent<Polymorph>();
            var BeastShapeIBuffSuppressBuffs = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponents<SuppressBuffs>();
            var BeastShapeShamblingMoundBuff = Resources.GetBlueprint<BlueprintBuff>("50ab9c820eb9cf94d8efba3632ad5ce2");
            var BeastShapeShamblingMoundBuffPolymorph = Resources.GetBlueprint<BlueprintBuff>("50ab9c820eb9cf94d8efba3632ad5ce2").GetComponent<Polymorph>();
            var MandragoraBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("d11108d2f1662a842929f53e16bd6742");
            var TreantBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("bb9ffa4bd65336f4f99ebd3a234f90cf");
            var FlytrapBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("6400a869e4026f242af0c3da506ecdd6");



            var PlantShapeIBuff = Helpers.CreateBuff("PlantShapeIBuff", bp => {
                bp.SetName("Plant Shape (Mandragora)");
                bp.SetDescription("You are in mandragora form now. You have a +2 size bonus to Dexterity and  Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also have one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.m_Icon = PlantShapeIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Prefab = new UnitViewLink() { AssetId = "ce880f58967fb4f4290586c57955883d" };
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_KeepSlots = false;
                    c.Size = Size.Small;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 0;
                    c.DexterityBonus = 2;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 2;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = Slam1d4.ToReference<BlueprintItemWeaponReference>();
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {                        
                        Slam1d4.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        MandragoraPoisonFeature.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = MandragoraBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIBuff = Helpers.CreateBuff("PlantShapeIIBuff", bp => {
                bp.SetName("Plant Shape (Shambling Mound)");
                bp.m_Description = BeastShapeShamblingMoundBuff.m_Description;
                bp.m_Icon = PlantShapeIIIcon;
                bp.Components = BeastShapeShamblingMoundBuff.Components;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIITreantBuff = Helpers.CreateBuff("PlantShapeIIITreantBuff", bp => {
                bp.SetName("Plant Shape (Treant)");
                bp.SetDescription("You are in treant form now. You have a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 natural " +
                    "armor bonus. You also have two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.m_Icon = PlantShapeIIIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Prefab = new UnitViewLink() { AssetId = "16f5bf5f4dc3c9e4dab4165b360a5e3d" };
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_KeepSlots = false;
                    c.Size = Size.Huge;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 8;
                    c.DexterityBonus = -2;
                    c.ConstitutionBonus = 4;
                    c.NaturalArmor = 6;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = SlamGargantuan2n6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = SlamGargantuan2n6.ToReference<BlueprintItemWeaponReference>();
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        DRSlashing10.ToReference<BlueprintUnitFactReference>(),
                        FireVulnerability.ToReference<BlueprintUnitFactReference>(),
                        OverrunAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeShamblingMoundBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeShamblingMoundBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeShamblingMoundBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = TreantBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIIGiantFlytrapBuff = Helpers.CreateBuff("PlantShapeIIIGiantFlytrapBuff", bp => {
                bp.SetName("Plant Shape (Giant Flytrap)");
                bp.SetDescription("You are in giant flytrap form now. You have a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 natural " +
                    "armor bonus. You also have four 1d8 bite attacks, acid Resistance 20, immunity to trip and blindsight.");
                bp.m_Icon = PlantShapeIIIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Prefab = new UnitViewLink() { AssetId = "c091d2aca0b6c3c45bbcc3d9a5394c7a" };
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_KeepSlots = false;
                    c.Size = Size.Huge;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 8;
                    c.DexterityBonus = -2;
                    c.ConstitutionBonus = 4;
                    c.NaturalArmor = 6;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = BiteLarge1d8.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = BiteLarge1d8.ToReference<BlueprintItemWeaponReference>();
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        BiteLarge1d8.ToReference<BlueprintItemWeaponReference>(),
                        BiteLarge1d8.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        DRSlashing10.ToReference<BlueprintUnitFactReference>(),
                        FireVulnerability.ToReference<BlueprintUnitFactReference>(),
                        OverrunAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeShamblingMoundBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeShamblingMoundBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeShamblingMoundBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = FlytrapBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var PlantShapeIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIAbility", bp => {
                bp.SetName("Plant Shape I");
                bp.SetDescription("You become a small mandragora. You gain a +2 size bonus to your Dexterity and Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also gain one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(                                                
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {PlantShapeIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIAbility", bp => {
                bp.SetName("Plant Shape II");
                bp.SetDescription("You become a large shambling mound. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, and resist electricity 20. Your movement speed is reduced by 10 feet. You also have two 2d6 slam attacks, the constricting " +
                    "vines ability, and the poison ability.\nConstricting Vines: A shambling mound's vines coil around any creature it hits with a slam attack. The shambling " +
                    "mound attempts a grapple maneuver check against its target, and on a successful check its vines deal 2d6+5 damage and the foe is grappled.\nGrappled " +
                    "characters cannot move, and take a -2 penalty on all attack rolls and a -4 penalty to Dexterity. Grappled characters attempt to escape every round by " +
                    "making a successful combat maneuver, Strength, Athletics, or Mobility check. The DC of this check is the shambling mound's CMD.\nEach round, creatures " +
                    "grappled by a shambling mound suffer 4d6+Strength modifier × 2 damage.\nA shambling mound receives a +4 bonus on grapple maneuver checks.\nPoison:\nSlam; " +
                    "Save: Fortitude\nFrequency: 1/round for 2 rounds\nEffect: 1d2 Strength and 1d2 Dexterity damage\nCure: 1 save\nThe save DC is Constitution-based.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIITreantAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIITreantAbility", bp => {
                bp.SetName("Plant Shape III (Treant)");
                bp.SetDescription("You become a huge treant. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIITreantBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIITreantBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIITreantAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIIGiantFlytrapAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIIGiantFlytrapAbility", bp => {
                bp.SetName("Plant Shape III (Giant Flytrap)");
                bp.SetDescription("You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIIGiantFlytrapAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIIAbility", bp => {
                bp.SetName("Plant Shape III");
                bp.SetDescription("You become a Huge Treant or a Huge Giant Flytrap.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        PlantShapeIIITreantAbility.ToReference<BlueprintAbilityReference>(),
                        PlantShapeIIIGiantFlytrapAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            PlantShapeIIITreantAbility.m_Parent = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();
            PlantShapeIIIGiantFlytrapAbility.m_Parent = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();

            var PlantShapeIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeI", Icon_ScrollOfPlantShapeI, PlantShapeIAbility, 5, 9);
            VenderTools.AddScrollToLeveledVenders(PlantShapeIScroll);
            PlantShapeIAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 5);
            PlantShapeIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 5);

            var PlantShapeIIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeII", Icon_ScrollOfPlantShapeII, PlantShapeIIAbility, 6, 11);
            VenderTools.AddScrollToLeveledVenders(PlantShapeIIScroll);
            PlantShapeIIAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 6);
            PlantShapeIIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 6);

            var PlantShapeIIIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeIII", Icon_ScrollOfPlantShapeIII, PlantShapeIIIAbility, 7, 13);
            VenderTools.AddScrollToLeveledVenders(PlantShapeIIIScroll);
            PlantShapeIIIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 7);
        }
    }
}
