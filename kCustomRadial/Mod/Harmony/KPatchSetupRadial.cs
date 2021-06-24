﻿using HarmonyLib;
using GUI_2;
using System;
using System.Collections.Generic;
using UnityEngine;
using DMT;
using kScripts;
using kCustomRadial.Mod.Scripts;


[HarmonyPatch]
public class KPatchCustomRadial
{

	[HarmonyPatch(typeof(ItemActionAttack))]
	[HarmonyPatch("SetupRadial")]
	public static bool Prefix(ItemActionAttack __instance, XUiC_Radial _xuiRadialWindow,
		EntityPlayerLocal _epl)
	{
		KRadial.KSetupRadial(_xuiRadialWindow, _epl);
		// _xuiRadialWindow.OnDoubleClick += KOnDoubleClicked;
		return false; // no need to return true because KRadial performs all tasks
	}
	
	[HarmonyPatch(typeof(XUiC_Radial))]
	[HarmonyPatch("ResetRadialEntries")]
	
	
	public static void Postfix(XUiC_Radial __instance)
	{
		__instance.Init();
	}

	[HarmonyPatch(typeof(XUiC_Radial))]
	[HarmonyPatch("Init")]
	
	
	public static void Postfix(XUiC_Radial __instance, ref List<XUiC_RadialEntry> ___menuItem)
	{
		KEventHandler.AssignHandler(ref ___menuItem);
	}

	
	[HarmonyPatch(typeof(XUiController))]
	[HarmonyPatch("DoubleClicked")]

	public static void Postfix(XUiController __instance, int mouseButton)
	{
		KEventHandler.KDoubleClicked();
	}
	
	[HarmonyPatch(typeof(XUiC_Radial))]
	[HarmonyPatch("XUiC_Radial_OnHover")]
	
	public static void Postfix(XUiC_Radial __instance, XUiController _sender, OnHoverEventArgs _e)
	{
		XUiC_RadialEntry xuiC_RadialEntry = (XUiC_RadialEntry)_sender;
		KHelper.EasyLog($"Isover: {_e.IsOver}");
		KEventHandler.KOnHover( __instance, _sender, _e);



	}
	
	[HarmonyPatch(typeof(XUiC_Radial))]
	[HarmonyPatch("XUiC_Radial_OnPress")]
	public static void Postfix(XUiC_Radial __instance, XUiC_RadialEntry _sender)
	{
		
	}
	
	/*
	[HarmonyPatch(typeof(XUiController))]
	[HarmonyPatch("OnDoubleClicked")]
	public static void Postfix(XUiController __instance, OnPressEventArgs _e)
	{
		
		
			KHelper.EasyLog("Double Clicked!", LogLevel.Chat);
	
	}
	*/
	

}
