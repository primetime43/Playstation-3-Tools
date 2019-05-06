using System;
using System.Text;
using System.Threading;
using BOIZMRPC;
using PS3Util;
using PS3Lib;
using System.IO;

namespace Black_Ops_1_Zombie_Console
{
    class ModMethods
    {
        private static PS3API PS3 = new PS3API();

        public static string Mapname = "";
        private const uint Cbuff = 0x00395BA8;
        private const uint SV_GSSC = 0x003C33A8;

        public static void Cbuf_AddText(string command)
        {
            Rpc.Call(Cbuff, 0, command);
        }

        public static void SV_GameSendServerCommand(int client, string command)
        {
            Rpc.Call(SV_GSSC, client, 1, command);
        }

        public static void OnScreenCenterText(int client, string text)
        {
            Rpc.Call(SV_GSSC, client, 1, "c \"" + text + "\"");
        }

        public static void SetDvars(int client, string dvar)//fix
        {
            Rpc.Call(SV_GSSC, client, 1, "v \"" + dvar + "\"");
        }

        public static void SetModel(int client, string model)
        {
            Rpc.Call(0x00305940, (uint)(0xFA805C + client * 0x34C), model);
        }

        public static void ModPoints(int client)
        {
            client = 0x0110090C + (client * 0x1d30);
            Console.WriteLine("Current Points: " + PS3A.GetInt32((ulong)client));
            Console.Write("\nEnter Desired Points: ");
            int moddedPoints = Convert.ToInt32(Console.ReadLine());
            PS3A.SetInt32((ulong)client, moddedPoints);
            Console.WriteLine("Points Modded To " + moddedPoints + "!");
            Thread.Sleep(1000);
        }

        public static void ListWeapons()
        {
            string[] weapon = new string[] {};
            switch (Mapname)
            {
                case "zombie_theater"://Kino Der Toten
                    weapon = new[] { "none", "defaultweapon", "zombie_bullet_crouch", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mp40_zm", "mp40_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "thundergun_zm", "thundergun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "m1911_zm", "rpk_zm", "mk_aug_upgraded_zm", "mk_aug_upgraded_zm", "hs10_zm", "g11_lps_upgraded_zm", "l96a1_upgraded_zm" };
                    break;
                case "zombie_pentagon"://Five
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "freezegun_zm", "freezegun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "minigun_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "zombie_gunstolen", "cz75dw_zm", "knife_ballistic_zm", "pm63_upgraded_zm", "claymore_zm", "hs10_upgraded_zm", "hs10_upgraded_zm", "zombie_perk_bottle_doubletap", "mp5k_zm", "ak74u_zm" };
                    break;
                case "zombietro"://Dead Ops Arcade
                    weapon = new[] { "none", "defaultweapon", "turret_bullet_zt", "t55_mini_turret", "hind_minigun_pilot_zt", "hind_rockets_zt", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "m2_flamethrower_zt", "ray_gun_zt", "m60_zt", "minigun_zt", "spas_zt", "china_lake_zt", "rpg_zt", "", "", "", "", "t55_mg", "", "", "", "", "", "", "", "", "", "", "", "", "m1911_zm", "spas_zt", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "hind_minigun_pilot_zt", "", "turret_bullet_zt", "m2_flamethrower_zt", "rpg_zt", "", "turret_bullet_zt", "", "", "", "spas_zt", "", "", "", "", "", "", "", "hind_rockets_zt", "", "", "hind_minigun_pilot_zt", "", "", "", "default_vehicle_weapon", "", "", "china_lake_zt", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "m1911_zm" };
                    break;
                case "zombie_cod5_prototype"://Nacht Der Untoten
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_m1carbine", "zombie_thompson", "zombie_kar98k", "kar98k_scoped_zombie", "stielhandgranate", "zombie_doublebarrel", "zombie_doublebarrel_sawed", "zombie_shotgun", "zombie_bar", "thundergun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "pm63_upgraded_zm", "m14_upgraded_zm", "thundergun_upgraded_zm", "cz75lh_zm", "pm63_upgraded_zm", "m1911_zm", "china_lake_upgraded_zm", "ak47_zm", "zombie_shotgun", "claymore_zm", "m14_zm", "zombie_m1carbine", "m72_law_zm", "ray_gun_upgraded_zm", "g11_lps_zm", "dragunov_zm", "l96a1_upgraded_zm", "ray_gun_zm", "zombie_perk_bottle_additionalprimaryweapon", "mk_aug_upgraded_zm", "dragunov_upgraded_zm", "zombie_kar98k", "ak74u_upgraded_zm", "aug_acog_mk_upgraded_zm", "m16_zm", "commando_upgraded_zm", "fnfal_zm", "m16_gl_upgraded_zm", "knife_ballistic_bowie_upgraded_zm", "famas_zm", "zombie_bowie_flourish", "knife_zm", "zombie_cymbal_monkey", "bowie_knife_zm", "default_vehicle_weapon", "explosive_bolt_upgraded_zm", "g11_lps_upgraded_zm", "zombie_perk_bottle_revive", "mk_commando_upgraded_zm", "zombie_bar", "zombie_doublebarrel_sawed", "cz75_upgraded_zm", "cz75_upgraded_zm", "crossbow_explosive_upgraded_zm", "m16_zm", "aug_acog_zm" };
                    break;
                case "zombie_cod5_asylum"://Verruckt
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_kar98k", "stielhandgranate", "zombie_gewehr43", "zombie_m1garand", "zombie_thompson", "zombie_shotgun", "mp40_zm", "zombie_bar_bipod", "zombie_stg44", "zombie_doublebarrel", "zombie_doublebarrel_sawed", "freezegun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "knife_ballistic_upgraded_zm", "zombie_knuckle_crack", "ithaca_zm", "defaultweapon", "default_vehicle_weapon", "cz75_zm", "rpk_upgraded_zm", "commando_zm", "hs10_zm", "ak74u_zm", "m72_law_zm", "python_zm", "galil_zm", "bowie_knife_zm", "m72_law_upgraded_zm", "", "galil_upgraded_zm", "zombie_perk_bottle_doubletap", "ray_gun_upgraded_zm", "knife_ballistic_zm", "aug_acog_zm", "hs10lh_upgraded_zm", "l96a1_upgraded_zm", "explosive_bolt_zm", "hs10_upgraded_zm", "hk21_zm", "m1911lh_upgraded_zm", "mp5k_zm", "ak74u_upgraded_zm", "mp40_zm", "rottweil72_zm", "zombie_m1garand", "python_upgraded_zm", "m1911_upgraded_zm", "knife_zm", "pm63_zm", "spas_zm", "mpl_zm", "fnfal_zm", "zombie_stg44", "zombie_stg44", "crossbow_explosive_zm", "l96a1_upgraded_zm", "cz75_upgraded_zm" };
                    break;
                case "zombie_cod5_sumpf"://Shi No Numa
                    weapon = new[] { "none", "defaultweapon", "dog_bite_zm", "m1911_zm", "frag_grenade_zm", "stielhandgranate", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_type99_rifle", "zombie_m1carbine", "zombie_m1garand", "zombie_gewehr43", "zombie_stg44", "zombie_thompson", "mp40_zm", "zombie_type100_smg", "zombie_shotgun", "zombie_bar", "tesla_gun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "m1911lh_upgraded_zm", "galil_upgraded_zm", "fnfal_upgraded_zm", "rottweil72_upgraded_zm", "python_upgraded_zm", "gl_m16_upgraded_zm", "m16_gl_upgraded_zm", "pm63lh_upgraded_zm", "syrette_sp", "knife_zm", "ak74u_zm", "m14_upgraded_zm", "zombie_perk_bottle_doubletap", "zombie_m1garand", "tesla_gun_upgraded_zm", "", "m14_zm", "mpl_upgraded_zm", "m72_law_zm", "stielhandgranate", "zombie_m1carbine", "hk21_upgraded_zm", "bowie_knife_zm", "zombie_perk_bottle_revive", "knife_ballistic_bowie_upgraded_zm", "l96a1_zm", "rpk_upgraded_zm", "zombie_perk_bottle_sleight", "mp5k_zm", "china_lake_zm", "aug_acog_mk_upgraded_zm", "mk_commando_upgraded_zm", "cz75lh_upgraded_zm", "dog_bite_zm", "mp40_zm", "cz75dw_upgraded_zm", "g11_lps_upgraded_zm", "zombie_thompson", "hs10_upgraded_zm", "rpk_zm", "rpk_zm", "crossbow_explosive_zm", "bowie_knife_zm", "zombie_shotgun" };
                    break;
                case "zombie_cod5_factory"://Der Reise
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "crossbow_explosive_zm", "knife_ballistic_zm", "knife_ballistic_bowie_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_upgraded_zm", "cz75_upgraded_zm", "g11_lps_upgraded_zm", "famas_upgraded_zm", "spectre_upgraded_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "spas_upgraded_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_upgraded_zm", "commando_upgraded_zm", "fnfal_upgraded_zm", "dragunov_upgraded_zm", "l96a1_upgraded_zm", "rpk_upgraded_zm", "hk21_upgraded_zm", "m72_law_upgraded_zm", "china_lake_upgraded_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_upgraded_zm", "zombie_kar98k", "zombie_kar98k_upgraded", "zombie_m1carbine", "zombie_m1carbine_upgraded", "zombie_gewehr43", "zombie_gewehr43_upgraded", "zombie_stg44", "zombie_stg44_upgraded", "zombie_thompson", "zombie_thompson_upgraded", "mp40_zm", "mp40_upgraded_zm", "zombie_type100_smg", "zombie_type100_smg_upgraded", "stielhandgranate", "zombie_doublebarrel", "zombie_doublebarrel_upgraded", "zombie_shotgun", "zombie_shotgun_upgraded", "zombie_fg42", "zombie_fg42_upgraded", "ray_gun_zm", "ray_gun_upgraded_zm", "tesla_gun_zm", "tesla_gun_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "spas_upgraded_zm", "hs10_upgraded_zm", "hs10_upgraded_zm", "m72_law_zm", "tesla_gun_zm" };
                    break;
                case "zombie_cosmodrome"://Ascension 
                    weapon = new[] { "none", "defaultweapon", "zombie_bullet_crouch", "m1911_zm", "frag_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_black_hole_bomb", "zombie_nesting_dolls", "ray_gun_zm", "ray_gun_upgraded_zm", "thundergun_zm", "thundergun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_sickle_zm", "knife_ballistic_sickle_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "minigun_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "sickle_knife_zm", "zombie_sickle_flourish", "zombie_nesting_doll_single", "zombie_nesting_doll_single", "g11_lps_zm", "famas_zm", "python_upgraded_zm" };
                    break;
                case "zombie_coast"://Call Of The Dead
                    Console.WriteLine("To be added!");
                    break;
                case "zombie_temple"://Shangri-la
                    weapon = new[] { "none", "defaultweapon", "m1911_zm", "frag_grenade_zm", "sticky_grenade_zm", "spikemore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "shrink_ray_zm", "shrink_ray_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_deadshot", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "ak74u_upgraded_zm", "pm63lh_upgraded_zm", "spas_upgraded_zm", "fnfal_zm", "aug_acog_zm", "m16_zm", "zombie_perk_bottle_additionalprimaryweapon" };
                    break;
                case "zombie_moon"://Moon
                    weapon = new[] { "none", "defaultweapon", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "sticky_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mp40_zm", "mp40_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "zombie_black_hole_bomb", "ray_gun_zm", "ray_gun_upgraded_zm", "zombie_quantum_bomb", "microwavegundw_zm", "microwavegunlh_zm", "microwavegun_zm", "microwavegundw_upgraded_zm", "microwavegunlh_upgraded_zm", "microwavegun_upgraded_zm", "minigun_zm", "equip_gasmask_zm", "equip_hacker_zm", "lower_equip_gasmask_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_deadshot", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish" };
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
            for (int i = 0; i < weapon.Length; i++)
            {
                Console.WriteLine(i + " - " + weapon[i] + " ");
            }
        }

        public static void GetWeapon(int weaponId)
        {
            string[] weapon = new string[] { };
            switch (Mapname)
            {
                case "zombie_theater"://Kino Der Toten
                    weapon = new[] { "none", "defaultweapon", "zombie_bullet_crouch", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mp40_zm", "mp40_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "thundergun_zm", "thundergun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "m1911_zm", "rpk_zm", "mk_aug_upgraded_zm", "mk_aug_upgraded_zm", "hs10_zm", "g11_lps_upgraded_zm", "l96a1_upgraded_zm" };
                    break;
                case "zombie_pentagon"://Five
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "freezegun_zm", "freezegun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "minigun_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "zombie_gunstolen", "cz75dw_zm", "knife_ballistic_zm", "pm63_upgraded_zm", "claymore_zm", "hs10_upgraded_zm", "hs10_upgraded_zm", "zombie_perk_bottle_doubletap", "mp5k_zm", "ak74u_zm" };
                    break;
                case "zombietro"://Dead Ops Arcade
                    weapon = new[] { "none", "defaultweapon", "turret_bullet_zt", "t55_mini_turret", "hind_minigun_pilot_zt", "hind_rockets_zt", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "m2_flamethrower_zt", "ray_gun_zt", "m60_zt", "minigun_zt", "spas_zt", "china_lake_zt", "rpg_zt", "", "", "", "", "t55_mg", "", "", "", "", "", "", "", "", "", "", "", "", "m1911_zm", "spas_zt", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "hind_minigun_pilot_zt", "", "turret_bullet_zt", "m2_flamethrower_zt", "rpg_zt", "", "turret_bullet_zt", "", "", "", "spas_zt", "", "", "", "", "", "", "", "hind_rockets_zt", "", "", "hind_minigun_pilot_zt", "", "", "", "default_vehicle_weapon", "", "", "china_lake_zt", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "m1911_zm" };
                    break;
                case "zombie_cod5_prototype"://Nacht Der Untoten
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_m1carbine", "zombie_thompson", "zombie_kar98k", "kar98k_scoped_zombie", "stielhandgranate", "zombie_doublebarrel", "zombie_doublebarrel_sawed", "zombie_shotgun", "zombie_bar", "thundergun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "pm63_upgraded_zm", "m14_upgraded_zm", "thundergun_upgraded_zm", "cz75lh_zm", "pm63_upgraded_zm", "m1911_zm", "china_lake_upgraded_zm", "ak47_zm", "zombie_shotgun", "claymore_zm", "m14_zm", "zombie_m1carbine", "m72_law_zm", "ray_gun_upgraded_zm", "g11_lps_zm", "dragunov_zm", "l96a1_upgraded_zm", "ray_gun_zm", "zombie_perk_bottle_additionalprimaryweapon", "mk_aug_upgraded_zm", "dragunov_upgraded_zm", "zombie_kar98k", "ak74u_upgraded_zm", "aug_acog_mk_upgraded_zm", "m16_zm", "commando_upgraded_zm", "fnfal_zm", "m16_gl_upgraded_zm", "knife_ballistic_bowie_upgraded_zm", "famas_zm", "zombie_bowie_flourish", "knife_zm", "zombie_cymbal_monkey", "bowie_knife_zm", "default_vehicle_weapon", "explosive_bolt_upgraded_zm", "g11_lps_upgraded_zm", "zombie_perk_bottle_revive", "mk_commando_upgraded_zm", "zombie_bar", "zombie_doublebarrel_sawed", "cz75_upgraded_zm", "cz75_upgraded_zm", "crossbow_explosive_upgraded_zm", "m16_zm", "aug_acog_zm" };
                    break;
                case "zombie_cod5_asylum"://Verruckt
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_kar98k", "stielhandgranate", "zombie_gewehr43", "zombie_m1garand", "zombie_thompson", "zombie_shotgun", "mp40_zm", "zombie_bar_bipod", "zombie_stg44", "zombie_doublebarrel", "zombie_doublebarrel_sawed", "freezegun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "knife_ballistic_upgraded_zm", "zombie_knuckle_crack", "ithaca_zm", "defaultweapon", "default_vehicle_weapon", "cz75_zm", "rpk_upgraded_zm", "commando_zm", "hs10_zm", "ak74u_zm", "m72_law_zm", "python_zm", "galil_zm", "bowie_knife_zm", "m72_law_upgraded_zm", "", "galil_upgraded_zm", "zombie_perk_bottle_doubletap", "ray_gun_upgraded_zm", "knife_ballistic_zm", "aug_acog_zm", "hs10lh_upgraded_zm", "l96a1_upgraded_zm", "explosive_bolt_zm", "hs10_upgraded_zm", "hk21_zm", "m1911lh_upgraded_zm", "mp5k_zm", "ak74u_upgraded_zm", "mp40_zm", "rottweil72_zm", "zombie_m1garand", "python_upgraded_zm", "m1911_upgraded_zm", "knife_zm", "pm63_zm", "spas_zm", "mpl_zm", "fnfal_zm", "zombie_stg44", "zombie_stg44", "crossbow_explosive_zm", "l96a1_upgraded_zm", "cz75_upgraded_zm" };
                    break;
                case "zombie_cod5_sumpf"://Shi No Numa
                    weapon = new[] { "none", "defaultweapon", "dog_bite_zm", "m1911_zm", "frag_grenade_zm", "stielhandgranate", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "ray_gun_zm", "crossbow_explosive_zm", "knife_ballistic_zm", "zombie_type99_rifle", "zombie_m1carbine", "zombie_m1garand", "zombie_gewehr43", "zombie_stg44", "zombie_thompson", "mp40_zm", "zombie_type100_smg", "zombie_shotgun", "zombie_bar", "tesla_gun_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "m1911lh_upgraded_zm", "galil_upgraded_zm", "fnfal_upgraded_zm", "rottweil72_upgraded_zm", "python_upgraded_zm", "gl_m16_upgraded_zm", "m16_gl_upgraded_zm", "pm63lh_upgraded_zm", "syrette_sp", "knife_zm", "ak74u_zm", "m14_upgraded_zm", "zombie_perk_bottle_doubletap", "zombie_m1garand", "tesla_gun_upgraded_zm", "", "m14_zm", "mpl_upgraded_zm", "m72_law_zm", "stielhandgranate", "zombie_m1carbine", "hk21_upgraded_zm", "bowie_knife_zm", "zombie_perk_bottle_revive", "knife_ballistic_bowie_upgraded_zm", "l96a1_zm", "rpk_upgraded_zm", "zombie_perk_bottle_sleight", "mp5k_zm", "china_lake_zm", "aug_acog_mk_upgraded_zm", "mk_commando_upgraded_zm", "cz75lh_upgraded_zm", "dog_bite_zm", "mp40_zm", "cz75dw_upgraded_zm", "g11_lps_upgraded_zm", "zombie_thompson", "hs10_upgraded_zm", "rpk_zm", "rpk_zm", "crossbow_explosive_zm", "bowie_knife_zm", "zombie_shotgun" };
                    break;
                case "zombie_cod5_factory"://Der Reise
                    weapon = new[] { "none", "defaultweapon", "ak47_zm", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "python_zm", "cz75_zm", "g11_lps_zm", "famas_zm", "spectre_zm", "cz75dw_zm", "cz75lh_zm", "spas_zm", "hs10_zm", "aug_acog_zm", "galil_zm", "commando_zm", "fnfal_zm", "dragunov_zm", "l96a1_zm", "rpk_zm", "hk21_zm", "m72_law_zm", "china_lake_zm", "zombie_cymbal_monkey", "crossbow_explosive_zm", "knife_ballistic_zm", "knife_ballistic_bowie_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_upgraded_zm", "cz75_upgraded_zm", "g11_lps_upgraded_zm", "famas_upgraded_zm", "spectre_upgraded_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "spas_upgraded_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_upgraded_zm", "commando_upgraded_zm", "fnfal_upgraded_zm", "dragunov_upgraded_zm", "l96a1_upgraded_zm", "rpk_upgraded_zm", "hk21_upgraded_zm", "m72_law_upgraded_zm", "china_lake_upgraded_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_upgraded_zm", "zombie_kar98k", "zombie_kar98k_upgraded", "zombie_m1carbine", "zombie_m1carbine_upgraded", "zombie_gewehr43", "zombie_gewehr43_upgraded", "zombie_stg44", "zombie_stg44_upgraded", "zombie_thompson", "zombie_thompson_upgraded", "mp40_zm", "mp40_upgraded_zm", "zombie_type100_smg", "zombie_type100_smg_upgraded", "stielhandgranate", "zombie_doublebarrel", "zombie_doublebarrel_upgraded", "zombie_shotgun", "zombie_shotgun_upgraded", "zombie_fg42", "zombie_fg42_upgraded", "ray_gun_zm", "ray_gun_upgraded_zm", "tesla_gun_zm", "tesla_gun_upgraded_zm", "mine_bouncing_betty", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "spas_upgraded_zm", "hs10_upgraded_zm", "hs10_upgraded_zm", "m72_law_zm", "tesla_gun_zm" };
                    break;
                case "zombie_cosmodrome"://Ascension 
                    weapon = new[] { "none", "defaultweapon", "zombie_bullet_crouch", "m1911_zm", "frag_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_black_hole_bomb", "zombie_nesting_dolls", "ray_gun_zm", "ray_gun_upgraded_zm", "thundergun_zm", "thundergun_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_sickle_zm", "knife_ballistic_sickle_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "minigun_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "sickle_knife_zm", "zombie_sickle_flourish", "zombie_nesting_doll_single", "zombie_nesting_doll_single", "g11_lps_zm", "famas_zm", "python_upgraded_zm" };
                    break;
                case "zombie_coast"://Call Of The Dead
                    Console.WriteLine("To be added!");
                    break;
                case "zombie_temple"://Shangri-la
                    weapon = new[] { "none", "defaultweapon", "m1911_zm", "frag_grenade_zm", "sticky_grenade_zm", "spikemore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "zombie_cymbal_monkey", "ray_gun_zm", "ray_gun_upgraded_zm", "shrink_ray_zm", "shrink_ray_upgraded_zm", "crossbow_explosive_zm", "crossbow_explosive_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "explosive_bolt_zm", "explosive_bolt_upgraded_zm", "claymore_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_deadshot", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish", "ak74u_upgraded_zm", "pm63lh_upgraded_zm", "spas_upgraded_zm", "fnfal_zm", "aug_acog_zm", "m16_zm", "zombie_perk_bottle_additionalprimaryweapon" };
                    break;
                case "zombie_moon"://Moon
                    weapon = new[] { "none", "defaultweapon", "m1911_zm", "frag_grenade_zm", "dog_bite_zm", "sticky_grenade_zm", "claymore_zm", "m1911_upgraded_zm", "m1911lh_upgraded_zm", "python_zm", "python_upgraded_zm", "cz75_zm", "cz75_upgraded_zm", "m14_zm", "m14_upgraded_zm", "m16_zm", "m16_gl_upgraded_zm", "gl_m16_upgraded_zm", "g11_lps_zm", "g11_lps_upgraded_zm", "famas_zm", "famas_upgraded_zm", "ak74u_zm", "ak74u_upgraded_zm", "mp5k_zm", "mp5k_upgraded_zm", "mp40_zm", "mp40_upgraded_zm", "mpl_zm", "mpl_upgraded_zm", "pm63_zm", "pm63_upgraded_zm", "pm63lh_upgraded_zm", "spectre_zm", "spectre_upgraded_zm", "cz75dw_zm", "cz75lh_zm", "cz75dw_upgraded_zm", "cz75lh_upgraded_zm", "ithaca_zm", "ithaca_upgraded_zm", "rottweil72_zm", "rottweil72_upgraded_zm", "spas_zm", "spas_upgraded_zm", "hs10_zm", "hs10_upgraded_zm", "hs10lh_upgraded_zm", "aug_acog_zm", "aug_acog_mk_upgraded_zm", "mk_aug_upgraded_zm", "galil_zm", "galil_upgraded_zm", "commando_zm", "commando_upgraded_zm", "fnfal_zm", "fnfal_upgraded_zm", "dragunov_zm", "dragunov_upgraded_zm", "l96a1_zm", "l96a1_upgraded_zm", "rpk_zm", "rpk_upgraded_zm", "hk21_zm", "hk21_upgraded_zm", "m72_law_zm", "m72_law_upgraded_zm", "china_lake_zm", "china_lake_upgraded_zm", "knife_ballistic_zm", "knife_ballistic_upgraded_zm", "knife_ballistic_bowie_zm", "knife_ballistic_bowie_upgraded_zm", "zombie_black_hole_bomb", "ray_gun_zm", "ray_gun_upgraded_zm", "zombie_quantum_bomb", "microwavegundw_zm", "microwavegunlh_zm", "microwavegun_zm", "microwavegundw_upgraded_zm", "microwavegunlh_upgraded_zm", "microwavegun_upgraded_zm", "minigun_zm", "equip_gasmask_zm", "equip_hacker_zm", "lower_equip_gasmask_zm", "knife_zm", "bowie_knife_zm", "syrette_sp", "zombie_perk_bottle_doubletap", "zombie_perk_bottle_jugg", "zombie_perk_bottle_revive", "zombie_perk_bottle_sleight", "zombie_knuckle_crack", "zombie_perk_bottle_marathon", "zombie_perk_bottle_nuke", "zombie_perk_bottle_deadshot", "zombie_perk_bottle_additionalprimaryweapon", "zombie_bowie_flourish" };
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            } Cbuf_AddText(" give " + weapon[weaponId]);
            Console.WriteLine(weapon[weaponId] + " has been given to client");
            Thread.Sleep(3000);
        }

        public static string GetMapName()
        {
            Mapname = PS3A.ReadString(0x0138E1B8);
            return Mapname;
        }

        public static void MotdEditor()
        {
            string motd = PS3A.ReadString(0x01c8004c);
            Console.WriteLine("Current MOTD: " + motd);

            Console.Write("\nEnter new MOTD: ");
            string newMOTD = Console.ReadLine();
            byte[] motdNew = Encoding.ASCII.GetBytes(newMOTD + '\0');
            PS3A.SetMemory(0x01c8004c, motdNew);
            Console.WriteLine("MOTD Changed To " + newMOTD + "!");
            Thread.Sleep(1000);
        }

        public static void PsnNameEditor()
        {
            string psn = PS3A.ReadString(0x01C33DB0);
            Console.WriteLine("Current PSN: " + psn);

            Console.Write("Enter new PSN name: ");
            string psnName = Console.ReadLine();
            byte[] name = Encoding.ASCII.GetBytes(psnName + '\0'); //PSN Name
            PS3A.SetMemory(0x01C33DB0, name);
            Console.WriteLine("PSN Name Changed To " + psnName + "!");
            Thread.Sleep(1000);
        }

        public static void ModKills(int client)
        {
            client = 0x01100910 + (client * 0x1d30);
            Console.WriteLine("Current Kills: " + PS3A.GetInt32((ulong)client));
            Console.Write("\nEnter Desired Kills: ");
            int moddedKills = Convert.ToInt32(Console.ReadLine());
            PS3A.SetInt32((ulong)client, moddedKills);
            Console.WriteLine("Kills Modded To " + moddedKills + "!");
            Thread.Sleep(1000);
        }

        public static void ModHeadshots(int client)
        {
            client = 0x01100930 + (client * 0x1d30);
            Console.WriteLine("Current Headshots: " + PS3A.GetInt32((ulong)client));
            Console.Write("\nEnter Desired Headshots: ");
            int moddedHeadshots = Convert.ToInt32(Console.ReadLine());
            PS3A.SetInt32((ulong)client, moddedHeadshots);
            Console.WriteLine("Headshots Modded To " + moddedHeadshots + "!");
            Thread.Sleep(1000);
        }

        public static void TakeEverything()
        {
            Cbuf_AddText(" take all");
            Console.WriteLine("All has been taken from client");
            Thread.Sleep(3000);
        }

        public static void GiveAllWeapons()
        {
            Cbuf_AddText(" give all");
            Console.WriteLine("All weapons have been given to client");
            Thread.Sleep(3000);
        }

        public static void SkipRounds()
        {
            Console.WriteLine("Rounds skipper has been activated!\n");
            Rpc.Call(0x00395BA8, 0, "set timescale 10");//increases game play speed
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    Rpc.Call(0x00395BA8, 0, "ai axis delete"); //kills all zombies
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            Rpc.Call(0x00395BA8, 0, "set timescale 1");
            Console.WriteLine("Rounds skipper has been deactivated!\n");
            Thread.Sleep(2000);
        }

        public static void DropWeapon()
        {
            Cbuf_AddText("dropweapon");
            Console.WriteLine("Weapon Dropped!");
            Thread.Sleep(1000);
        }

        public static void GodMode()
        {
            Cbuf_AddText("god");
            Console.WriteLine("God Mode");
            Thread.Sleep(1000);
        }

        public static void NoClip()
        {
            Cbuf_AddText("noclip");
            Console.WriteLine("No Clip Mode");
            Thread.Sleep(1000);
        }

        public static void Ufo()
        {
            Cbuf_AddText("ufo");
            Console.WriteLine("UFO Mode");
            Thread.Sleep(1000);
        }

        public static void NoTarget()
        {
            Cbuf_AddText("notarget");
            Console.WriteLine("No Target Mode");
            Thread.Sleep(1000);
        }

        public static void loadImages()
        {
            Console.WriteLine("Thanks to aerosoul94 for the image pool address!");
            Thread.Sleep(2000);
            const int start_add = 0x00c1d75c;
            String fileString = "";
            String path = "Black Ops 1 Images.txt";
            int max = (0x00c5d32c - 0x00c1d75c)/112;
            int counter = 1;
            for (int i = start_add; i < 0x00c5d32c; i += 0x70)
            {
                Console.Clear();
                Console.WriteLine("Retrieving images... " + counter + "/" + max );
                uint address = PS3A.FollowPointer((uint)i);
                string image = PS3A.ReadString(address);
                fileString += "Image Address: 0x" + i.ToString("X") + "\nImage: " + image + "\nImage Stored Location Address: 0x" + (i - 0x3c).ToString("X") + "\nImage Size: 0x" + PS3A.GetInt16((ulong)(i - 0x4a)).ToString("X") + "\nImage Width: " + PS3A.GetInt16((ulong)(i - 0x48)) + "\nImage Height: " + PS3A.GetInt16((ulong)(i - 0x46)) + "\n\n";
                counter += 1;
            }

            Console.WriteLine("\nWould you like to save the images to a .txt file? [yes/no]");
            String answer = Console.ReadLine();
            if (answer.ToLower() == "yes")
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(fileString);
                    writer.Dispose();
                }
                Console.WriteLine("Saved to a .txt file!");
            }
            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }

        public static void UnlockAllTrophies()//fix
        {
            string[] achievements = new string[74];
            achievements[0] = "8 SP_WIN_CUBA";
            achievements[1] = "8 SP_WIN_VORKUTA";
            achievements[2] = "8 SP_WIN_PENTAGON";
            achievements[3] = "8 SP_WIN_FLASHPOINT";
            achievements[4] = "8 SP_WIN_KHE_SANH";
            achievements[5] = "8 SP_WIN_HUE_CITY";
            achievements[6] = "8 SP_WIN_KOWLOON";
            achievements[7] = "8 SP_WIN_RIVER";
            achievements[8] = "8 SP_WIN_FULLAHEAD";
            achievements[9] = "8 SP_WIN_INTERROGATION_ESCAPE";
            achievements[10] = "8 SP_WIN_UNDERWATERBASE";
            achievements[11] = "8 SP_VWIN_FLASHPOINT";
            achievements[12] = "8 SP_VWIN_HUE_CITY";
            achievements[13] = "8 SP_VWIN_RIVER";
            achievements[14] = "8 SP_VWIN_FULLAHEAD";
            achievements[15] = "8 SP_VWIN_UNDERWATERBASE";
            achievements[16] = "8 SP_LVL_CUBA_CASTRO_ONESHOT";
            achievements[17] = "8 SP_LVL_VORKUTA_VEHICULAR";
            achievements[18] = "8 SP_LVL_VORKUTA_SLINGSHOT";
            achievements[19] = "8 SP_LVL_KHESANH_MISSILES";
            achievements[20] = "8 SP_LVL_HUECITY_AIRSUPPORT";
            achievements[21] = "8 SP_LVL_HUECITY_DRAGON";
            achievements[22] = "8 SP_LVL_CREEK1_DESTROY_MG";
            achievements[23] = "8 SP_LVL_CREEK1_KNIFING";
            achievements[24] = "8 SP_LVL_KOWLOON_DUAL";
            achievements[25] = "8 SP_LVL_RIVER_TARGETS";
            achievements[26] = "8 SP_LVL_WMD_RSO";
            achievements[27] = "8 SP_LVL_WMD_RELAY";
            achievements[28] = "8 SP_LVL_POW_HIND";
            achievements[29] = "8 SP_LVL_POW_FLAMETHROWER";
            achievements[30] = "8 SP_LVL_FULLAHEAD_2MIN";
            achievements[31] = "8 SP_LVL_REBIRTH_MONKEYS";
            achievements[32] = "8 SP_LVL_REBIRTH_NOLEAKS";
            achievements[33] = "8 SP_LVL_UNDERWATERBASE_MINI";
            achievements[34] = "8 SP_LVL_FRONTEND_CHAIR";
            achievements[35] = "8 SP_LVL_FRONTEND_ZORK";
            achievements[36] = "8 SP_GEN_MASTER";
            achievements[37] = "8 SP_GEN_FRAGMASTER";
            achievements[38] = "8 SP_GEN_ROUGH_ECO";
            achievements[39] = "8 SP_GEN_CROSSBOW";
            achievements[40] = "8 SP_GEN_FOUNDFILMS";
            achievements[41] = "8 SP_ZOM_COLLECTOR";
            achievements[42] = "8 SP_ZOM_NODAMAGE";
            achievements[43] = "8 SP_ZOM_TRAPS";
            achievements[44] = "8 SP_ZOM_SILVERBACK";
            achievements[45] = "8 SP_ZOM_CHICKENS";
            achievements[46] = "8 SP_ZOM_FLAMINGBULL";
            achievements[47] = "8 MP_FILM_CREATED";
            achievements[48] = "8 MP_WAGER_MATCH";
            achievements[49] = "8 MP_PLAY";
            achievements[50] = "8 DLC1_ZOM_OLDTIMER";
            achievements[51] = "8 DLC1_ZOM_HARDWAY";
            achievements[52] = "8 DLC1_ZOM_PISTOLERO";
            achievements[53] = "8 DLC1_ZOM_BIGBADDABOOM";
            achievements[54] = "8 DLC1_ZOM_NOLEGS";
            achievements[55] = "8 DLC2_ZOM_PROTECTEQUIP";
            achievements[56] = "8 DLC2_ZOM_LUNARLANDERS";
            achievements[57] = "8 DLC2_ZOM_FIREMONKEY";
            achievements[58] = "8 DLC2_ZOM_BLACKHOLE";
            achievements[59] = "8 DLC2_ZOM_PACKAPUNCH";
            achievements[60] = "8 DLC3_ZOM_STUNTMAN";
            achievements[61] = "8 DLC3_ZOM_SHOOTING_ON_LOCATION";
            achievements[62] = "8 DLC3_ZOM_QUIET_ON_THE_SET";
            achievements[63] = "8 DLC4_ZOM_TEMPLE_SIDEQUEST";
            achievements[64] = "8 DLC5_ZOM_CRYOGENIC_PARTY";
            achievements[65] = "8 DLC5_ZOM_BIG_BANG_THEORY";
            achievements[66] = "8 DLC5_ZOM_GROUND_CONTROL";
            achievements[67] = "8 DLC5_ZOM_ONE_SMALL_HACK";
            achievements[68] = "8 DLC5_ZOM_ONE_GIANT_LEAP";
            achievements[69] = "8 DLC5_ZOM_PERKS_IN_SPACE";
            achievements[70] = "8 DLC5_ZOM_FULLY_ARMED";
            achievements[71] = "8 DLC4_ZOM_ZOMB_DISPOSAL";
            achievements[72] = "8 DLC4_ZOM_MONKEY_SEE_MONKEY_DONT";
            achievements[73] = "8 DLC4_ZOM_BLINDED_BY_THE_FRIGHT";
            achievements[74] = "8 DLC4_ZOM_SMALL_CONSOLATION";




            byte[] FT111 = new byte[] { 0x41 };
            byte[] FT11 = new byte[] { 0x40 };
            byte[] RPCON1 = new byte[] { 0x38, 0x60, 0xFF, 0xFF, 0x38, 0x80, 0x00, 0x00, 0x3C, 0xA0, 0x02, 0x00, 0x30, 0xA5, 0x50, 0x00, 0x4B, 0xFB, 0xAE, 0xB5, 0x4B, 0xFF, 0xF0, 0x60 };
            byte[] RPCOFF1 = new byte[] { 0x82, 0xF8, 0x00, 0x00, 0x3E, 0xA0, 0x00, 0xB5, 0x3D, 0x20, 0x00, 0x82, 0x56, 0xE0, 0x18, 0x38, 0x56, 0xE4, 0x38, 0x30, 0x3B, 0x95, 0x6C, 0x50, 0x7F, 0x60, 0x20, 0x50, 0x38, 0x80, 0x00, 0x00 };




            for (int i = 0; i < 74; i++)
            {
                PS3.SetMemory(0x2005000, Encoding.ASCII.GetBytes(achievements + "\0"));
                PS3.SetMemory(0x407554, FT111);
                PS3.SetMemory(0x4084E4, RPCON1);
                Thread.Sleep(15);
                PS3.SetMemory(0x407554, FT11);
                PS3.SetMemory(0x4084E4, RPCOFF1);
            }
            Console.WriteLine("Trophies unlocked! Press any buton to continue...");
            Console.ReadKey();
        }
    }
}
