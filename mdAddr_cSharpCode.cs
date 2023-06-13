using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace MelissaData {
	public class mdAddr : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdAddrUnmanaged {
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrCreate();
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrDestroy(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrInitialize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrInitialize(IntPtr i, string p1, string p2, string p3);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrInitializeDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrInitializeDataFiles(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetInitializeErrorString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrSetLicenseString(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetBuildNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDatabaseDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetExpirationDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetLicenseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCanadianDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCanadianDatabaseDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCanadianExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCanadianExpirationDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToUSFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToUSFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToCanadaFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToCanadaFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToDPVDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToDPVDataFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToLACSLinkDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToLACSLinkDataFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToSuiteLinkDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToSuiteLinkDataFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToSuiteFinderDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToSuiteFinderDataFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToRBDIFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToRBDIFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetRBDIDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetRBDIDatabaseDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPathToAddrKeyDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPathToAddrKeyDataFiles(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrClearProperties", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrClearProperties(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrResetDPV", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrResetDPV(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetCASSEnable", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetCASSEnable(IntPtr i, Int32 p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetUseUSPSPreferredCityNames", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetUseUSPSPreferredCityNames(IntPtr i, Int32 p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetDiacritics", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetDiacritics(IntPtr i, Int32 p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetStatusCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetStatusCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetErrorCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetErrorCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetErrorString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetResults", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetResults(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetResultCodeDescription", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetResultCodeDescription(IntPtr i, string resultCode, Int32 opt);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_B1_ProcessorName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_B1_ProcessorName(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_B4_ListName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_B4_ListName(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_Name", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_Name(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_Company", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_Company(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_Address", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_Address(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_City", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_City(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_State", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_State(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPS3553_D3_ZIP", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPS3553_D3_ZIP(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetFormPS3553", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetFormPS3553(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSaveFormPS3553", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrSaveFormPS3553(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrResetFormPS3553", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrResetFormPS3553(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrResetFormPS3553Counter", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrResetFormPS3553Counter(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetStandardizationType", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetStandardizationType(IntPtr i, Int32 mode);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetSuiteParseMode", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetSuiteParseMode(IntPtr i, Int32 mode);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetAliasMode", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetAliasMode(IntPtr i, Int32 mode);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetFormSOA", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetFormSOA(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSaveFormSOA", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSaveFormSOA(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrResetFormSOA", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrResetFormSOA(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetSOACustomerInfo", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetSOACustomerInfo(IntPtr i, string customerName, string customerAddress);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetSOACPCNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetSOACPCNumber(IntPtr i, string CPCNumber);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOACustomerInfo", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSOACustomerInfo(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOACPCNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSOACPCNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOATotalRecords", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetSOATotalRecords(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOAAAPercentage", CallingConvention = CallingConvention.Cdecl)]
			public static extern float mdAddrGetSOAAAPercentage(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOAAAExpiryDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSOAAAExpiryDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOASoftwareInfo", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSOASoftwareInfo(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSOAErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSOAErrorString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetCompany", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetCompany(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetLastName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetLastName(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetAddress", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetAddress(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetAddress2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetAddress2(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetLastLine", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetLastLine(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetSuite", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetSuite(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetCity", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetCity(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetState", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetState(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetZip(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetPlus4", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetPlus4(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetUrbanization", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetUrbanization(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedAddressRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedAddressRange(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedPreDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedPreDirection(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedStreetName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedStreetName(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedSuffix(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedPostDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedPostDirection(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedSuiteName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedSuiteName(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedSuiteRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedSuiteRange(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedRouteService", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedRouteService(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedLockBox", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedLockBox(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetParsedDeliveryInstallation", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetParsedDeliveryInstallation(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetCountryCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetCountryCode(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrVerifyAddress", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrVerifyAddress(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCompany", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCompany(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetLastName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetLastName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetAddress", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetAddress(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetAddress2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetAddress2(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSuite", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSuite(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCity", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCity(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCityAbbreviation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCityAbbreviation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetState", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetState(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetZip(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPlus4", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetPlus4(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCarrierRoute", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCarrierRoute(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDeliveryPointCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDeliveryPointCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDeliveryPointCheckDigit", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDeliveryPointCheckDigit(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCountyFips", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCountyFips(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCountyName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCountyName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetAddressTypeCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetAddressTypeCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetAddressTypeString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetAddressTypeString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetUrbanization", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetUrbanization(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCongressionalDistrict", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCongressionalDistrict(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetLACS", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetLACS(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetLACSLinkIndicator", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetLACSLinkIndicator(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPrivateMailbox", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetPrivateMailbox(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetTimeZoneCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetTimeZoneCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetTimeZone", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetTimeZone(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetMsa", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetMsa(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPmsa", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetPmsa(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDefaultFlagIndicator", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDefaultFlagIndicator(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSuiteStatus", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSuiteStatus(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetEWSFlag", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetEWSFlag(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCMRA", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCMRA(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDsfNoStats", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDsfNoStats(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDsfVacant", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDsfVacant(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDsfDNA", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDsfDNA(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetCountryCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetCountryCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZipType", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetZipType(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetFalseTable", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetFalseTable(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetDPVFootnotes", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetDPVFootnotes(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetLACSLinkReturnCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetLACSLinkReturnCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetSuiteLinkReturnCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetSuiteLinkReturnCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetRBDI", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetRBDI(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetELotNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetELotNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetELotOrder", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetELotOrder(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetAddressKey", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetAddressKey(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetMelissaAddressKey", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetMelissaAddressKey(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetMelissaAddressKeyBase", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetMelissaAddressKeyBase(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrFindSuggestion", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrFindSuggestion(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrFindSuggestionNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrFindSuggestionNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_B6_TotalRecords", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_B6_TotalRecords(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_C1a_ZIP4Coded", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_C1a_ZIP4Coded(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_C1c_DPBCAssigned", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_C1c_DPBCAssigned(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_C1d_FiveDigitCoded", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_C1d_FiveDigitCoded(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_C1e_CRRTCoded", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_C1e_CRRTCoded(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_C1f_eLOTAssigned", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_C1f_eLOTAssigned(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_HighRiseDefault", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_HighRiseDefault(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_HighRiseExact", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_HighRiseExact(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_RuralRouteDefault", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_RuralRouteDefault(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_RuralRouteExact", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_RuralRouteExact(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZip4HRDefault", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetZip4HRDefault(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZip4HRExact", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetZip4HRExact(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZip4RRDefault", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetZip4RRDefault(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetZip4RRExact", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetZip4RRExact(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_LACSCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_LACSCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_EWSCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_EWSCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_E_DPVCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_E_DPVCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_POBoxCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_POBoxCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_HCExactCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_HCExactCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_FirmCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_FirmCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_GenDeliveryCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_GenDeliveryCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_MilitaryZipCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_MilitaryZipCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_NonDeliveryCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_NonDeliveryCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_StreetCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_StreetCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_HCDefaultCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_HCDefaultCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_OtherCount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_OtherCount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_LacsLinkCodeACount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_LacsLinkCodeACount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_LacsLinkCode00Count", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_LacsLinkCode00Count(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_LacsLinkCode14Count", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_LacsLinkCode14Count(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_LacsLinkCode92Count", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_LacsLinkCode92Count(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_LacsLinkCode09Count", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_LacsLinkCode09Count(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_SuiteLinkCodeACount", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_SuiteLinkCodeACount(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetPS3553_X_SuiteLinkCode00Count", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdAddrGetPS3553_X_SuiteLinkCode00Count(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedAddressRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedAddressRange(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedPreDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedPreDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedStreetName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedStreetName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedSuffix(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedPostDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedPostDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedSuiteName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedSuiteName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedSuiteRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedSuiteRange(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedPrivateMailboxName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedPrivateMailboxName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedPrivateMailboxNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedPrivateMailboxNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedGarbage", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedGarbage(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedRouteService", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedRouteService(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedLockBox", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedLockBox(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetParsedDeliveryInstallation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetParsedDeliveryInstallation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrSetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdAddrSetReserved(IntPtr i, string p1, string p2);
			[DllImport("mdAddr.dll", EntryPoint = "mdAddrGetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdAddrGetReserved(IntPtr i, string p1);
		}

		public mdAddr() {
			i = mdAddrUnmanaged.mdAddrCreate();
		}

		~mdAddr() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdAddrUnmanaged.mdAddrDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public ProgramStatus Initialize(string p1, string p2, string p3) {
			return (ProgramStatus)mdAddrUnmanaged.mdAddrInitialize(i, p1, p2, p3);
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdAddrUnmanaged.mdAddrInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetInitializeErrorString(i));
		}

		public bool SetLicenseString(string p1) {
			return (mdAddrUnmanaged.mdAddrSetLicenseString(i, p1) != 0);
		}

		public string GetBuildNumber() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDatabaseDate(i));
		}

		public string GetExpirationDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetLicenseExpirationDate(i));
		}

		public string GetCanadianDatabaseDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCanadianDatabaseDate(i));
		}

		public string GetCanadianExpirationDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCanadianExpirationDate(i));
		}

		public void SetPathToUSFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToUSFiles(i, p1);
		}

		public void SetPathToCanadaFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToCanadaFiles(i, p1);
		}

		public void SetPathToDPVDataFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToDPVDataFiles(i, p1);
		}

		public void SetPathToLACSLinkDataFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToLACSLinkDataFiles(i, p1);
		}

		public void SetPathToSuiteLinkDataFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToSuiteLinkDataFiles(i, p1);
		}

		public void SetPathToSuiteFinderDataFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToSuiteFinderDataFiles(i, p1);
		}

		public void SetPathToRBDIFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToRBDIFiles(i, p1);
		}

		public string GetRBDIDatabaseDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetRBDIDatabaseDate(i));
		}

		public void SetPathToAddrKeyDataFiles(string p1) {
			mdAddrUnmanaged.mdAddrSetPathToAddrKeyDataFiles(i, p1);
		}

		public void ClearProperties() {
			mdAddrUnmanaged.mdAddrClearProperties(i);
		}

		public void ResetDPV() {
			mdAddrUnmanaged.mdAddrResetDPV(i);
		}

		public void SetCASSEnable(int p1) {
			mdAddrUnmanaged.mdAddrSetCASSEnable(i, p1);
		}

		public void SetUseUSPSPreferredCityNames(int p1) {
			mdAddrUnmanaged.mdAddrSetUseUSPSPreferredCityNames(i, p1);
		}

		public void SetDiacritics(DiacriticsMode p1) {
			mdAddrUnmanaged.mdAddrSetDiacritics(i, (int)p1);
		}

		public string GetStatusCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetStatusCode(i));
		}

		public string GetErrorCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetErrorCode(i));
		}

		public string GetErrorString() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetErrorString(i));
		}

		public string GetResults() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetResults(i));
		}

		public string GetResultCodeDescription(string resultCode, ResultCdDescOpt opt) {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetResultCodeDescription(i, resultCode, (int)opt));
		}

		public string GetResultCodeDescription(string resultCode) {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetResultCodeDescription(i, resultCode, (int)ResultCdDescOpt.ResultCodeDescriptionLong));
		}

		public void SetPS3553_B1_ProcessorName(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_B1_ProcessorName(i, p1);
		}

		public void SetPS3553_B4_ListName(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_B4_ListName(i, p1);
		}

		public void SetPS3553_D3_Name(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_Name(i, p1);
		}

		public void SetPS3553_D3_Company(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_Company(i, p1);
		}

		public void SetPS3553_D3_Address(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_Address(i, p1);
		}

		public void SetPS3553_D3_City(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_City(i, p1);
		}

		public void SetPS3553_D3_State(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_State(i, p1);
		}

		public void SetPS3553_D3_ZIP(string p1) {
			mdAddrUnmanaged.mdAddrSetPS3553_D3_ZIP(i, p1);
		}

		public string GetFormPS3553() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetFormPS3553(i));
		}

		public bool SaveFormPS3553(string p1) {
			return (mdAddrUnmanaged.mdAddrSaveFormPS3553(i, p1) != 0);
		}

		public void ResetFormPS3553() {
			mdAddrUnmanaged.mdAddrResetFormPS3553(i);
		}

		public void ResetFormPS3553Counter() {
			mdAddrUnmanaged.mdAddrResetFormPS3553Counter(i);
		}

		public void SetStandardizationType(StandardizeMode mode) {
			mdAddrUnmanaged.mdAddrSetStandardizationType(i, (int)mode);
		}

		public void SetSuiteParseMode(SuiteParseMode mode) {
			mdAddrUnmanaged.mdAddrSetSuiteParseMode(i, (int)mode);
		}

		public void SetAliasMode(AliasPreserveMode mode) {
			mdAddrUnmanaged.mdAddrSetAliasMode(i, (int)mode);
		}

		public string GetFormSOA() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetFormSOA(i));
		}

		public void SaveFormSOA(string p1) {
			mdAddrUnmanaged.mdAddrSaveFormSOA(i, p1);
		}

		public void ResetFormSOA() {
			mdAddrUnmanaged.mdAddrResetFormSOA(i);
		}

		public void SetSOACustomerInfo(string customerName, string customerAddress) {
			mdAddrUnmanaged.mdAddrSetSOACustomerInfo(i, customerName, customerAddress);
		}

		public void SetSOACPCNumber(string CPCNumber) {
			mdAddrUnmanaged.mdAddrSetSOACPCNumber(i, CPCNumber);
		}

		public string GetSOACustomerInfo() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSOACustomerInfo(i));
		}

		public string GetSOACPCNumber() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSOACPCNumber(i));
		}

		public int GetSOATotalRecords() {
			return mdAddrUnmanaged.mdAddrGetSOATotalRecords(i);
		}

		public float GetSOAAAPercentage() {
			return mdAddrUnmanaged.mdAddrGetSOAAAPercentage(i);
		}

		public string GetSOAAAExpiryDate() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSOAAAExpiryDate(i));
		}

		public string GetSOASoftwareInfo() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSOASoftwareInfo(i));
		}

		public string GetSOAErrorString() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSOAErrorString(i));
		}

		public void SetCompany(string p1) {
			mdAddrUnmanaged.mdAddrSetCompany(i, p1);
		}

		public void SetLastName(string p1) {
			mdAddrUnmanaged.mdAddrSetLastName(i, p1);
		}

		public void SetAddress(string p1) {
			mdAddrUnmanaged.mdAddrSetAddress(i, p1);
		}

		public void SetAddress2(string p1) {
			mdAddrUnmanaged.mdAddrSetAddress2(i, p1);
		}

		public void SetLastLine(string p1) {
			mdAddrUnmanaged.mdAddrSetLastLine(i, p1);
		}

		public void SetSuite(string p1) {
			mdAddrUnmanaged.mdAddrSetSuite(i, p1);
		}

		public void SetCity(string p1) {
			mdAddrUnmanaged.mdAddrSetCity(i, p1);
		}

		public void SetState(string p1) {
			mdAddrUnmanaged.mdAddrSetState(i, p1);
		}

		public void SetZip(string p1) {
			mdAddrUnmanaged.mdAddrSetZip(i, p1);
		}

		public void SetPlus4(string p1) {
			mdAddrUnmanaged.mdAddrSetPlus4(i, p1);
		}

		public void SetUrbanization(string p1) {
			mdAddrUnmanaged.mdAddrSetUrbanization(i, p1);
		}

		public void SetParsedAddressRange(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedAddressRange(i, p1);
		}

		public void SetParsedPreDirection(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedPreDirection(i, p1);
		}

		public void SetParsedStreetName(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedStreetName(i, p1);
		}

		public void SetParsedSuffix(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedSuffix(i, p1);
		}

		public void SetParsedPostDirection(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedPostDirection(i, p1);
		}

		public void SetParsedSuiteName(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedSuiteName(i, p1);
		}

		public void SetParsedSuiteRange(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedSuiteRange(i, p1);
		}

		public void SetParsedRouteService(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedRouteService(i, p1);
		}

		public void SetParsedLockBox(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedLockBox(i, p1);
		}

		public void SetParsedDeliveryInstallation(string p1) {
			mdAddrUnmanaged.mdAddrSetParsedDeliveryInstallation(i, p1);
		}

		public void SetCountryCode(string p1) {
			mdAddrUnmanaged.mdAddrSetCountryCode(i, p1);
		}

		public bool VerifyAddress() {
			return (mdAddrUnmanaged.mdAddrVerifyAddress(i) != 0);
		}

		public string GetCompany() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCompany(i));
		}

		public string GetLastName() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetLastName(i));
		}

		public string GetAddress() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetAddress(i));
		}

		public string GetAddress2() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetAddress2(i));
		}

		public string GetSuite() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSuite(i));
		}

		public string GetCity() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCity(i));
		}

		public string GetCityAbbreviation() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCityAbbreviation(i));
		}

		public string GetState() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetState(i));
		}

		public string GetZip() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetZip(i));
		}

		public string GetPlus4() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetPlus4(i));
		}

		public string GetCarrierRoute() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCarrierRoute(i));
		}

		public string GetDeliveryPointCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDeliveryPointCode(i));
		}

		public string GetDeliveryPointCheckDigit() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDeliveryPointCheckDigit(i));
		}

		public string GetCountyFips() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCountyFips(i));
		}

		public string GetCountyName() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCountyName(i));
		}

		public string GetAddressTypeCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetAddressTypeCode(i));
		}

		public string GetAddressTypeString() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetAddressTypeString(i));
		}

		public string GetUrbanization() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetUrbanization(i));
		}

		public string GetCongressionalDistrict() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCongressionalDistrict(i));
		}

		public string GetLACS() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetLACS(i));
		}

		public string GetLACSLinkIndicator() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetLACSLinkIndicator(i));
		}

		public string GetPrivateMailbox() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetPrivateMailbox(i));
		}

		public string GetTimeZoneCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetTimeZoneCode(i));
		}

		public string GetTimeZone() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetTimeZone(i));
		}

		public string GetMsa() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetMsa(i));
		}

		public string GetPmsa() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetPmsa(i));
		}

		public string GetDefaultFlagIndicator() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDefaultFlagIndicator(i));
		}

		public string GetSuiteStatus() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSuiteStatus(i));
		}

		public string GetEWSFlag() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetEWSFlag(i));
		}

		public string GetCMRA() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCMRA(i));
		}

		public string GetDsfNoStats() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDsfNoStats(i));
		}

		public string GetDsfVacant() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDsfVacant(i));
		}

		public string GetDsfDNA() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDsfDNA(i));
		}

		public string GetCountryCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetCountryCode(i));
		}

		public string GetZipType() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetZipType(i));
		}

		public string GetFalseTable() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetFalseTable(i));
		}

		public string GetDPVFootnotes() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetDPVFootnotes(i));
		}

		public string GetLACSLinkReturnCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetLACSLinkReturnCode(i));
		}

		public string GetSuiteLinkReturnCode() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetSuiteLinkReturnCode(i));
		}

		public string GetRBDI() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetRBDI(i));
		}

		public string GetELotNumber() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetELotNumber(i));
		}

		public string GetELotOrder() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetELotOrder(i));
		}

		public string GetAddressKey() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetAddressKey(i));
		}

		public string GetMelissaAddressKey() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetMelissaAddressKey(i));
		}

		public string GetMelissaAddressKeyBase() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetMelissaAddressKeyBase(i));
		}

		public bool FindSuggestion() {
			return (mdAddrUnmanaged.mdAddrFindSuggestion(i) != 0);
		}

		public bool FindSuggestionNext() {
			return (mdAddrUnmanaged.mdAddrFindSuggestionNext(i) != 0);
		}

		public int GetPS3553_B6_TotalRecords() {
			return mdAddrUnmanaged.mdAddrGetPS3553_B6_TotalRecords(i);
		}

		public int GetPS3553_C1a_ZIP4Coded() {
			return mdAddrUnmanaged.mdAddrGetPS3553_C1a_ZIP4Coded(i);
		}

		public int GetPS3553_C1c_DPBCAssigned() {
			return mdAddrUnmanaged.mdAddrGetPS3553_C1c_DPBCAssigned(i);
		}

		public int GetPS3553_C1d_FiveDigitCoded() {
			return mdAddrUnmanaged.mdAddrGetPS3553_C1d_FiveDigitCoded(i);
		}

		public int GetPS3553_C1e_CRRTCoded() {
			return mdAddrUnmanaged.mdAddrGetPS3553_C1e_CRRTCoded(i);
		}

		public int GetPS3553_C1f_eLOTAssigned() {
			return mdAddrUnmanaged.mdAddrGetPS3553_C1f_eLOTAssigned(i);
		}

		public int GetPS3553_E_HighRiseDefault() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_HighRiseDefault(i);
		}

		public int GetPS3553_E_HighRiseExact() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_HighRiseExact(i);
		}

		public int GetPS3553_E_RuralRouteDefault() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_RuralRouteDefault(i);
		}

		public int GetPS3553_E_RuralRouteExact() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_RuralRouteExact(i);
		}

		public int GetZip4HRDefault() {
			return mdAddrUnmanaged.mdAddrGetZip4HRDefault(i);
		}

		public int GetZip4HRExact() {
			return mdAddrUnmanaged.mdAddrGetZip4HRExact(i);
		}

		public int GetZip4RRDefault() {
			return mdAddrUnmanaged.mdAddrGetZip4RRDefault(i);
		}

		public int GetZip4RRExact() {
			return mdAddrUnmanaged.mdAddrGetZip4RRExact(i);
		}

		public int GetPS3553_E_LACSCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_LACSCount(i);
		}

		public int GetPS3553_E_EWSCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_EWSCount(i);
		}

		public int GetPS3553_E_DPVCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_E_DPVCount(i);
		}

		public int GetPS3553_X_POBoxCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_POBoxCount(i);
		}

		public int GetPS3553_X_HCExactCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_HCExactCount(i);
		}

		public int GetPS3553_X_FirmCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_FirmCount(i);
		}

		public int GetPS3553_X_GenDeliveryCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_GenDeliveryCount(i);
		}

		public int GetPS3553_X_MilitaryZipCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_MilitaryZipCount(i);
		}

		public int GetPS3553_X_NonDeliveryCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_NonDeliveryCount(i);
		}

		public int GetPS3553_X_StreetCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_StreetCount(i);
		}

		public int GetPS3553_X_HCDefaultCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_HCDefaultCount(i);
		}

		public int GetPS3553_X_OtherCount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_OtherCount(i);
		}

		public int GetPS3553_X_LacsLinkCodeACount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_LacsLinkCodeACount(i);
		}

		public int GetPS3553_X_LacsLinkCode00Count() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_LacsLinkCode00Count(i);
		}

		public int GetPS3553_X_LacsLinkCode14Count() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_LacsLinkCode14Count(i);
		}

		public int GetPS3553_X_LacsLinkCode92Count() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_LacsLinkCode92Count(i);
		}

		public int GetPS3553_X_LacsLinkCode09Count() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_LacsLinkCode09Count(i);
		}

		public int GetPS3553_X_SuiteLinkCodeACount() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_SuiteLinkCodeACount(i);
		}

		public int GetPS3553_X_SuiteLinkCode00Count() {
			return mdAddrUnmanaged.mdAddrGetPS3553_X_SuiteLinkCode00Count(i);
		}

		public string GetParsedAddressRange() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedAddressRange(i));
		}

		public string GetParsedPreDirection() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedPreDirection(i));
		}

		public string GetParsedStreetName() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedStreetName(i));
		}

		public string GetParsedSuffix() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedSuffix(i));
		}

		public string GetParsedPostDirection() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedPostDirection(i));
		}

		public string GetParsedSuiteName() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedSuiteName(i));
		}

		public string GetParsedSuiteRange() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedSuiteRange(i));
		}

		public string GetParsedPrivateMailboxName() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedPrivateMailboxName(i));
		}

		public string GetParsedPrivateMailboxNumber() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedPrivateMailboxNumber(i));
		}

		public string GetParsedGarbage() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedGarbage(i));
		}

		public string GetParsedRouteService() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedRouteService(i));
		}

		public string GetParsedLockBox() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedLockBox(i));
		}

		public string GetParsedDeliveryInstallation() {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetParsedDeliveryInstallation(i));
		}

		public void SetReserved(string p1, string p2) {
			mdAddrUnmanaged.mdAddrSetReserved(i, p1, p2);
		}

		public string GetReserved(string p1) {
			return Marshal.PtrToStringAnsi(mdAddrUnmanaged.mdAddrGetReserved(i, p1));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdParse : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdParseUnmanaged {
			[DllImport("mdAddr.dll", EntryPoint = "mdParseCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseCreate();
			[DllImport("mdAddr.dll", EntryPoint = "mdParseDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdParseDestroy(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseInitialize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdParseInitialize(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetBuildNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseParse", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdParseParse(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseParseCanadian", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdParseParseCanadian(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseParseNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdParseParseNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseLastLineParse", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdParseLastLineParse(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetZip(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetPlus4", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetPlus4(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetCity", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetCity(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetState", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetState(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetStreetName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetStreetName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetRange(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetPreDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetPreDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetPostDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetPostDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetSuffix(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetSuiteName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetSuiteName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetSuiteNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetSuiteNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetPrivateMailboxNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetPrivateMailboxNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetPrivateMailboxName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetPrivateMailboxName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetGarbage", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetGarbage(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetRouteService", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetRouteService(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetLockBox", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetLockBox(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseGetDeliveryInstallation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdParseGetDeliveryInstallation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdParseParseRule", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdParseParseRule(IntPtr i);
		}

		public mdParse() {
			i = mdParseUnmanaged.mdParseCreate();
		}

		~mdParse() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdParseUnmanaged.mdParseDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public ProgramStatus Initialize(string p1) {
			return (ProgramStatus)mdParseUnmanaged.mdParseInitialize(i, p1);
		}

		public string GetBuildNumber() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetBuildNumber(i));
		}

		public void Parse(string p1) {
			mdParseUnmanaged.mdParseParse(i, p1);
		}

		public void ParseCanadian(string p1) {
			mdParseUnmanaged.mdParseParseCanadian(i, p1);
		}

		public bool ParseNext() {
			return (mdParseUnmanaged.mdParseParseNext(i) != 0);
		}

		public void LastLineParse(string p1) {
			mdParseUnmanaged.mdParseLastLineParse(i, p1);
		}

		public string GetZip() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetZip(i));
		}

		public string GetPlus4() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetPlus4(i));
		}

		public string GetCity() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetCity(i));
		}

		public string GetState() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetState(i));
		}

		public string GetStreetName() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetStreetName(i));
		}

		public string GetRange() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetRange(i));
		}

		public string GetPreDirection() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetPreDirection(i));
		}

		public string GetPostDirection() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetPostDirection(i));
		}

		public string GetSuffix() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetSuffix(i));
		}

		public string GetSuiteName() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetSuiteName(i));
		}

		public string GetSuiteNumber() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetSuiteNumber(i));
		}

		public string GetPrivateMailboxNumber() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetPrivateMailboxNumber(i));
		}

		public string GetPrivateMailboxName() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetPrivateMailboxName(i));
		}

		public string GetGarbage() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetGarbage(i));
		}

		public string GetRouteService() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetRouteService(i));
		}

		public string GetLockBox() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetLockBox(i));
		}

		public string GetDeliveryInstallation() {
			return Marshal.PtrToStringAnsi(mdParseUnmanaged.mdParseGetDeliveryInstallation(i));
		}

		public int ParseRule() {
			return mdParseUnmanaged.mdParseParseRule(i);
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdStreet : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdStreetUnmanaged {
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetCreate();
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdStreetDestroy(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetInitialize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetInitialize(IntPtr i, string p1, string p2, string p3);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetInitializeErrorString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetDatabaseDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetBuildNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetSetLicenseString(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetLicenseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetFindStreet", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetFindStreet(IntPtr i, string p1, string p2, Int32 p3);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetFindStreetNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetFindStreetNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetIsAddressInRange", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetIsAddressInRange(IntPtr i, string p1, string p2, string p3);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetIsAddressInRange2", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdStreetIsAddressInRange2(IntPtr i, string p1, string p2, string p3, string p4);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetAutoCompletion", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetAutoCompletion(IntPtr i, string p1, string p2, Int32 p3, Int32 p4);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetResetAutoCompletion", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdStreetResetAutoCompletion(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetBaseAlternateIndicator", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetBaseAlternateIndicator(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetLACSIndicator", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetLACSIndicator(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetUrbanizationCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetUrbanizationCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetUrbanizationName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetUrbanizationName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetLastLineNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetLastLineNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetAddressType", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetAddressType(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetCongressionalDistrict", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetCongressionalDistrict(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetCountyFips", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetCountyFips(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetCompany", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetCompany(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetCarrierRoute", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetCarrierRoute(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetZip(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetDeliveryInstallation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetDeliveryInstallation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPlus4High", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPlus4High(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPlus4Low", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPlus4Low(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetSuiteRangeOddEven", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetSuiteRangeOddEven(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetSuiteRangeHigh", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetSuiteRangeHigh(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetSuiteRangeLow", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetSuiteRangeLow(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetSuiteName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetSuiteName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPostDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPostDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetSuffix(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetStreetName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetStreetName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPreDirection", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPreDirection(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPrimaryRangeOddEven", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPrimaryRangeOddEven(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPrimaryRangeHigh", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPrimaryRangeHigh(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdStreetGetPrimaryRangeLow", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdStreetGetPrimaryRangeLow(IntPtr i);
		}

		public mdStreet() {
			i = mdStreetUnmanaged.mdStreetCreate();
		}

		~mdStreet() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdStreetUnmanaged.mdStreetDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public ProgramStatus Initialize(string p1, string p2, string p3) {
			return (ProgramStatus)mdStreetUnmanaged.mdStreetInitialize(i, p1, p2, p3);
		}

		public string GetInitializeErrorString() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetInitializeErrorString(i));
		}

		public string GetDatabaseDate() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetDatabaseDate(i));
		}

		public string GetBuildNumber() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetBuildNumber(i));
		}

		public bool SetLicenseString(string p1) {
			return (mdStreetUnmanaged.mdStreetSetLicenseString(i, p1) != 0);
		}

		public string GetLicenseExpirationDate() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetLicenseExpirationDate(i));
		}

		public bool FindStreet(string p1, string p2, bool p3) {
			return (mdStreetUnmanaged.mdStreetFindStreet(i, p1, p2, (p3 ? 1 : 0)) != 0);
		}

		public bool FindStreetNext() {
			return (mdStreetUnmanaged.mdStreetFindStreetNext(i) != 0);
		}

		public bool IsAddressInRange(string p1, string p2, string p3) {
			return (mdStreetUnmanaged.mdStreetIsAddressInRange(i, p1, p2, p3) != 0);
		}

		public bool IsAddressInRange2(string p1, string p2, string p3, string p4) {
			return (mdStreetUnmanaged.mdStreetIsAddressInRange2(i, p1, p2, p3, p4) != 0);
		}

		public string GetAutoCompletion(string p1, string p2, AutoCompletionMode p3, bool p4) {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetAutoCompletion(i, p1, p2, (int)p3, (p4 ? 1 : 0)));
		}

		public void ResetAutoCompletion() {
			mdStreetUnmanaged.mdStreetResetAutoCompletion(i);
		}

		public string GetBaseAlternateIndicator() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetBaseAlternateIndicator(i));
		}

		public string GetLACSIndicator() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetLACSIndicator(i));
		}

		public string GetUrbanizationCode() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetUrbanizationCode(i));
		}

		public string GetUrbanizationName() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetUrbanizationName(i));
		}

		public string GetLastLineNumber() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetLastLineNumber(i));
		}

		public string GetAddressType() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetAddressType(i));
		}

		public string GetCongressionalDistrict() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetCongressionalDistrict(i));
		}

		public string GetCountyFips() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetCountyFips(i));
		}

		public string GetCompany() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetCompany(i));
		}

		public string GetCarrierRoute() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetCarrierRoute(i));
		}

		public string GetZip() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetZip(i));
		}

		public string GetDeliveryInstallation() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetDeliveryInstallation(i));
		}

		public string GetPlus4High() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPlus4High(i));
		}

		public string GetPlus4Low() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPlus4Low(i));
		}

		public string GetSuiteRangeOddEven() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetSuiteRangeOddEven(i));
		}

		public string GetSuiteRangeHigh() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetSuiteRangeHigh(i));
		}

		public string GetSuiteRangeLow() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetSuiteRangeLow(i));
		}

		public string GetSuiteName() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetSuiteName(i));
		}

		public string GetPostDirection() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPostDirection(i));
		}

		public string GetSuffix() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetSuffix(i));
		}

		public string GetStreetName() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetStreetName(i));
		}

		public string GetPreDirection() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPreDirection(i));
		}

		public string GetPrimaryRangeOddEven() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPrimaryRangeOddEven(i));
		}

		public string GetPrimaryRangeHigh() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPrimaryRangeHigh(i));
		}

		public string GetPrimaryRangeLow() {
			return Marshal.PtrToStringAnsi(mdStreetUnmanaged.mdStreetGetPrimaryRangeLow(i));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}

	public class mdZip : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdZipUnmanaged {
			[DllImport("mdAddr.dll", EntryPoint = "mdZipCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipCreate();
			[DllImport("mdAddr.dll", EntryPoint = "mdZipDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdZipDestroy(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipInitialize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipInitialize(IntPtr i, string p1, string p2, string p3);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetInitializeErrorString(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetDatabaseDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetBuildNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipSetLicenseString(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetLicenseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindZip(IntPtr i, string p1, Int32 p2);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindZipNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindZipNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindZipInCity", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindZipInCity(IntPtr i, string p1, string p2);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindZipInCityNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindZipInCityNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindCityInState", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindCityInState(IntPtr i, string p1, string p2);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipFindCityInStateNext", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipFindCityInStateNext(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipComputeDistance", CallingConvention = CallingConvention.Cdecl)]
			public static extern double mdZipComputeDistance(IntPtr i, double p1, double p2, double p3, double p4);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipComputeBearing", CallingConvention = CallingConvention.Cdecl)]
			public static extern double mdZipComputeBearing(IntPtr i, double p1, double p2, double p3, double p4);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetCountyNameFromFips", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetCountyNameFromFips(IntPtr i, string p1);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetSCFArea", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdZipGetSCFArea(IntPtr i, string p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetZip", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetZip(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetCity", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetCity(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetCityAbbreviation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetCityAbbreviation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetState", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetState(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetZipType", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetZipType(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetCountyName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetCountyName(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetCountyFips", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetCountyFips(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetAreaCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetAreaCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetLongitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetLongitude(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetLatitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetLatitude(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetTimeZone", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetTimeZone(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetTimeZoneCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetTimeZoneCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetMsa", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetMsa(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetPmsa", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetPmsa(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetFacilityCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetFacilityCode(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetLastLineIndicator", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetLastLineIndicator(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetLastLineNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetLastLineNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetPreferredLastLineNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetPreferredLastLineNumber(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetAutomation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetAutomation(IntPtr i);
			[DllImport("mdAddr.dll", EntryPoint = "mdZipGetFinanceNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdZipGetFinanceNumber(IntPtr i);
		}

		public mdZip() {
			i = mdZipUnmanaged.mdZipCreate();
		}

		~mdZip() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdZipUnmanaged.mdZipDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public ProgramStatus Initialize(string p1, string p2, string p3) {
			return (ProgramStatus)mdZipUnmanaged.mdZipInitialize(i, p1, p2, p3);
		}

		public string GetInitializeErrorString() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetInitializeErrorString(i));
		}

		public string GetDatabaseDate() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetDatabaseDate(i));
		}

		public string GetBuildNumber() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetBuildNumber(i));
		}

		public bool SetLicenseString(string p1) {
			return (mdZipUnmanaged.mdZipSetLicenseString(i, p1) != 0);
		}

		public string GetLicenseExpirationDate() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetLicenseExpirationDate(i));
		}

		public bool FindZip(string p1, bool p2) {
			return (mdZipUnmanaged.mdZipFindZip(i, p1, (p2 ? 1 : 0)) != 0);
		}

		public bool FindZipNext() {
			return (mdZipUnmanaged.mdZipFindZipNext(i) != 0);
		}

		public bool FindZipInCity(string p1, string p2) {
			return (mdZipUnmanaged.mdZipFindZipInCity(i, p1, p2) != 0);
		}

		public bool FindZipInCityNext() {
			return (mdZipUnmanaged.mdZipFindZipInCityNext(i) != 0);
		}

		public bool FindCityInState(string p1, string p2) {
			return (mdZipUnmanaged.mdZipFindCityInState(i, p1, p2) != 0);
		}

		public bool FindCityInStateNext() {
			return (mdZipUnmanaged.mdZipFindCityInStateNext(i) != 0);
		}

		public double ComputeDistance(double p1, double p2, double p3, double p4) {
			return mdZipUnmanaged.mdZipComputeDistance(i, p1, p2, p3, p4);
		}

		public double ComputeBearing(double p1, double p2, double p3, double p4) {
			return mdZipUnmanaged.mdZipComputeBearing(i, p1, p2, p3, p4);
		}

		public string GetCountyNameFromFips(string p1) {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetCountyNameFromFips(i, p1));
		}

		public int GetSCFArea(string p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5) {
			return mdZipUnmanaged.mdZipGetSCFArea(i, p1, p2, p3, p4, p5);
		}

		public string GetZip() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetZip(i));
		}

		public string GetCity() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetCity(i));
		}

		public string GetCityAbbreviation() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetCityAbbreviation(i));
		}

		public string GetState() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetState(i));
		}

		public string GetZipType() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetZipType(i));
		}

		public string GetCountyName() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetCountyName(i));
		}

		public string GetCountyFips() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetCountyFips(i));
		}

		public string GetAreaCode() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetAreaCode(i));
		}

		public string GetLongitude() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetLongitude(i));
		}

		public string GetLatitude() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetLatitude(i));
		}

		public string GetTimeZone() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetTimeZone(i));
		}

		public string GetTimeZoneCode() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetTimeZoneCode(i));
		}

		public string GetMsa() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetMsa(i));
		}

		public string GetPmsa() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetPmsa(i));
		}

		public string GetFacilityCode() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetFacilityCode(i));
		}

		public string GetLastLineIndicator() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetLastLineIndicator(i));
		}

		public string GetLastLineNumber() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetLastLineNumber(i));
		}

		public string GetPreferredLastLineNumber() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetPreferredLastLineNumber(i));
		}

		public string GetAutomation() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetAutomation(i));
		}

		public string GetFinanceNumber() {
			return Marshal.PtrToStringAnsi(mdZipUnmanaged.mdZipGetFinanceNumber(i));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}
