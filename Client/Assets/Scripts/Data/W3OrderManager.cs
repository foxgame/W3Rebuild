using UnityEngine;
using System;
using System.Collections.Generic;

public enum W3OrderType
{
    attack = 1, //           Attack
    attackground, //         Attack Ground
    attackonce, //           Attack Once
    autoharvestgold, //      Harvest Nearby Gold
    autoharvestlumber, //    Harvest Nearby Lumber
    board, //                Board
    forceboard, //           Force Board
    harvest, //              Harvest
    holdposition, //         Hold Position
    load, //                 Load
    move, //                 Follow
    patrol, //               Patrol To
    revive, //               Revive Hero
    returnresources, //      Return Resources
    setrally, //             Set Rally Point To
    stop, //                 Stop
    unload, //               Unload
    unloadall, //            Unload All At
    avatar, //               Mountain King - Activate Avatar
    blizzard, //             Archmage - Blizzard
    defend, //               Footman - Defend
    undefend, //             Footman - Stop Defend
    dispel, //               Priest - Dispel
    divineshield, //         Paladin - Activate Divine Shield
    undivineshield, //       Paladin - Deactivate Divine Shield
    flare, //                Mortar Team - Flare
    heal, //                 Priest - Heal
    healon, //               Priest - Activate Heal
    healoff, //              Priest - Deactivate Heal
    holybolt, //             Paladin - Holy Light
    innerfire, //            Priest - Inner Fire
    innerfireon, //          Priest - Activate Inner Fire
    innerfireoff, //         Priest - Deactivate Inner Fire
    invisibility, //         Sorceress - Invisibility
    massteleport, //         Archmage - Mass Teleport
    militia, //              Peasant - Call To Arms
    militiaoff, //           Militia - Back To Work
    polymorph, //            Sorceress - Polymorph
    repair, //               Peasant - Repair
    repairon, //             Peasant - Activate Repair
    repairoff, //            Peasant - Deactivate Repair
    resurrection, //         Paladin - Resurrection
    slow, //                 Sorceress - Slow
    slowon, //               Sorceress - Activate Slow
    slowoff, //              Sorceress - Deactivate Slow
    thunderbolt, //          Mountain King - Storm Bolt
    thunderclap, //          Mountain King - Thunder Clap
    townbellon, //           Town Hall - Call To Arms
    townbelloff, //          Town Hall - Back To Work
    waterelemental, //       Archmage - Summon Water Elemental
    battlestations, //       Burrow - Battle Stations
    standdown, //            Burrow - Stand Down
    bloodlust, //            Shaman - Bloodlust
    bloodluston, //          Shaman - Activate Bloodlust
    bloodlustoff, //         Shaman - Deactivate Bloodlust
    chainlightning, //       Far Seer - Chain Lightning
    devour, //               Kodo Beast - Devour
    earthquake, //           Far Seer - Earthquake
    ensnare, //              Raider - Ensnare
    evileye, //              Witch Doctor - Sentry Ward
    farsight, //             Far Seer - Far Sight
    healingward, //          Witch Doctor - Healing Ward
    lightningshield, //      Shaman - Lightning Shield
    mirrorimage, //          Blademaster - Mirror Image
    purge, //                Shaman - Purge
//     repair, //               Peon - Repair
//     repairon, //             Peon - Activate Repair
//     repairoff, //            Peon - Deactivate Repair
    shockwave, //           Tauren Chieftain - Shockwave
    spiritwolf, //           Far Seer - Feral Spirit
    stasistrap, //           Witch Doctor - Stasis Trap
    stomp, //                Tauren Chieftain - War Stomp
    whirlwind, //            Blademaster - Bladestorm
    windwalk, //             Blademaster - Wind Walk
    renew, //                Wisp - Renew
    entangle, //             Tree Of Life - Entangle
    entangleinstant, //      Tree Of Life - Entangle (Instant) 
    replenish, //            Moon Well - Replenish Mana
    mounthippogryph, //      Archer - Mount Hippogryph
    loadarcher, //           Hippogryph - Pick Up Archer
    autodispel, //           Dryad - Abolish Magic
    faeriefire, //           Druid Of The Talon - Faerie Fire
    cyclone, //              Druid Of The Talon - Cyclone
    rejuvination, //         Druid Of The Claw - Rejuvenation
    manaburn, //             Demon Hunter - Mana Burn
    entanglingroots, //      Keeper Of The Grove - Entangling Roots
    root, //                 Ancients - Root
    detonate, //             Wisp - Detonate
    forceofnature, //        Keeper Of The Grove - Force Of Nature
    rainoffire, //           Priestess Of The Moon - Starfall
    eattree, //              Ancients - Eat Tree
    sentinel, //             Huntress - Sentinel
    unroot, //               Ancients - Uproot
    ambush, //               Sentinels - Hide
    renewon, //              Wisp - Activate Renew
    renewoff, //             Wisp - Deactivate Renew
    autodispelon, //         Dryad - Activate Abolish Magic
    autodispeloff, //        Dryad - Deactivate Abolish Magic
    faeriefireon, //         Druid Of The Talon - Activate Faerie Fire
    faeriefireoff, //        Druid Of The Talon - Deactivate Faerie Fire
    ravenform, //            Druid Of The Talon - Storm Crow Form
    unravenform, //          Druid Of The Talon - Night Elf Form
    roar, //                 Druid Of The Claw - Roar
    bearform, //             Druid Of The Claw - Bear Form
    unbearform, //           Druid Of The Claw - Night Elf Form
    immolation, //           Demon Hunter - Activate Immolation
    unimmolation, //         Demon Hunter - Deactivate Immolation
    metamorphosis, //        Demon Hunter - Metamorphosis
    tranquility, //          Keeper Of The Grove - Tranquility
    flamingarrows, //        Priestess Of The Moon - Activate Searing Arrows
    unflamingarrows, //      Priestess Of The Moon - Deactivate Searing Arrows
    scout, //                Priestess Of The Moon - Scout
    restoration, //          Acolyte - Restore
    sacrifice, //            Sacrificial Pit - Sacrifice
//    sacrifice, //            Acolyte - Sacrifice
    unsummon, //             Acolyte - Unsummon
    web, //                  Crypt Fiend - Web
    raisedead, //            Necromancer - Raise Dead
    unholyfrenzy, //         Necromancer - Unholy Frenzy
    cripple, //              Necromancer - Cripple
    curse, //                Banshee - Curse
    antimagicshell, //       Banshee - Anti-magic Shell
    possession, //           Banshee - Possession
    deathcoil, //            Death Knight - Death Coil
    deathpact, //            Death Knight - Death Pact
    sleep, //                Dreadlord - Sleep
    frostnova, //            Lich - Frost Nova
    frostarmor, //           Lich - Frost Armor
    frostarmoron, //         Lich - Activate Frost Armor
    frostarmoroff, //        Lich - Deactivate Frost Armor
    darkritual, //           Lich - Dark Ritual
    carrionswarm, //         Dreadlord - Carrion Swarm
    dreadlordinferno, //     Dreadlord - Inferno
    deathanddecay, //        Lich - Death And Decay
    restorationon, //        Acolyte - Activate Restore
    restorationoff, //       Acolyte - Deactivate Restore
    cannibalize, //          Ghoul - Cannibalize
    webon, //                Crypt Fiend - Activate Web
    weboff, //               Crypt Fiend - Deactivate Web
    loadcorpse, //           Meat Wagon - Get Corpse
    unloadallcorpses, //     Meat Wagon - Drop All Corpses
    stoneform, //            Gargoyle - Stone Form
    unstoneform, //          Gargoyle - Gargoyle Form
    raisedeadon, //          Necromancer - Activate Raise Skeleton
    raisedeadoff, //         Necromancer - Deactivate Raise Skeleton
    curseon, //              Banshee - Activate Curse
    curseoff, //             Banshee - Deactivate Curse
    animatedead, //          Death Knight - Animate Dead
    selfdestruct, //         Goblin Sappers - Kaboom! 
    revenge, //              Neutral - Revenge
    fingerofdeath, //        Archimonde - Finger Of Death
    darkconversion, //       Malganis - Dark Conversion
    soulpreservation, //     Malganis - Soul Preservation
//    darkconversion, //       Miscellaneous - Dark Conversion (Fast) 
    darkportal, //           Archimonde - Dark Portal
//    rainoffire, //           Archimonde - Rain Of Chaos
//    rainoffire, //           Doom Guard - Rain Of Fire
    inferno, //              Warlock - Inferno
    darksummoning, //        Special - Dark Summoning
//    ravenform, //            Medivh - Raven Form
//    unravenform, //          Medivh - Human Form
    coldarrows, //           Sylvanas Windrunner - Activate Cold Arrows
    uncoldarrows, //         Sylvanas Windrunner - Deactivate Cold Arrows


    cancel,
    builds,
    buildsCancel,
    trans,
    build,


}


public class W3Order
{
    public List<int> unitsID;
    public List<W3Unit> units;

    public W3OrderType type;
    public Vector3Int pos;
}

public class W3OrderManager : SingletonMono< W3OrderManager >
{
    public Queue<W3Order> data = new Queue<W3Order>();
    public W3Order order = null;

    int delay = 1;

    public override void initSingletonMono()
    {
    }

    public void addMoveQueue( List< W3Unit > list , int x , int z )
    {
        order = new W3Order();
        order.units = list ;
        order.type = W3OrderType.move;
        order.pos.x = -x;
        order.pos.z = -z;
    }

    void FixedUpdate()
    {
        data.Enqueue( order );
        order = null;

        if ( data.Count > delay )
        {
            doQueue();
        }
    }

    void doQueue()
    {
        W3Order order = data.Dequeue();

        if ( order == null )
        {
            return;
        }

        switch ( order.type )
        {
            case W3OrderType.move:
                {
                    for ( int i = 0 ; i < order.units.Count ; i++ )
                    {
                        if ( order.units[ i ].isBuilding )
                            break;

                        order.units[ i ].moveToNormal( order.pos.x , order.pos.z );

                        int x = (int)( order.pos.x / GameDefine.TERRAIN_SIZE_PER );
                        int z = (int)( order.pos.z / GameDefine.TERRAIN_SIZE_PER );

                        int uid = W3PathFinder.instance.getUnitID( x , z );

                        if ( uid > 0 && 
                            order.units[ i ].baseID != uid )
                        {
                            W3Unit unit = W3BaseManager.instance.getUnit( uid );

                            if ( !order.units[ i ].targets.Contains( unit ) )
                            {
                                order.units[ i ].targets.Add( unit );
                                unit.selection( true );
                            }

                        }
                    }
                }
                break;
            default:
                break;
        }
    }



}

