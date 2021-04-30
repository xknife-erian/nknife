// Copyright (C) 2010 OfficeSIP Communications
// This source is subject to the GNU General Public License.
// Please see Notice.txt for details.

using System;
using System.ComponentModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;

// ReSharper disable once CheckNamespace
namespace NKnife.Win
{
	#region Enums

	public enum SecurityStatus : uint
	{
		SecEOk = 0x00000000,
		SecEInsufficientMemory = 0x80090300,
		SecEInvalidHandle = 0x80090301,
		SecEUnsupportedFunction = 0x80090302,
		SecETargetUnknown = 0x80090303,
		SecEInternalError = 0x80090304,
		SecESecpkgNotFound = 0x80090305,
		SecENotOwner = 0x80090306,
		SecECannotInstall = 0x80090307,
		SecEInvalidToken = 0x80090308,
		SecECannotPack = 0x80090309,
		SecEQopNotSupported = 0x8009030A,
		SecENoImpersonation = 0x8009030B,
		SecELogonDenied = 0x8009030C,
		SecEUnknownCredentials = 0x8009030D,
		SecENoCredentials = 0x8009030E,
		SecEMessageAltered = 0x8009030F,
		SecEOutOfSequence = 0x80090310,
		SecENoAuthenticatingAuthority = 0x80090311,
		SecIContinueNeeded = 0x00090312,
		SecICompleteNeeded = 0x00090313,
		SecICompleteAndContinue = 0x00090314,
		SecILocalLogon = 0x00090315,
		SecEBadPkgid = 0x80090316,
		SecEContextExpired = 0x80090317,
		SecEIncompleteMessage = 0x80090318,
		SecEIncompleteCredentials = 0x80090320,
		SecEBufferTooSmall = 0x80090321,
		SecIIncompleteCredentials = 0x00090320,
		SecIRenegotiate = 0x00090321,
		SecEWrongPrincipal = 0x80090322,
		SecEAlgorithmMismatch = 0x80090331,
		SecEEncryptFailure = 0x80090329,
		SecEDecryptFailure = 0x80090330,

		SecEUnknowError = 0xFFFFFFFF,
	}

	public enum CredentialUse
	{
		SecpkgCredInbound = 1,
		SecpkgCredOutbound = 2,
		SecpkgCredBoth = 3,
	}

	public enum TargetDataRep
	{
		SecurityNetworkDrep = 0x00000000,
		SecurityNativeDrep = 0x00000010
	}

	public enum ContextReq
	{
		AscReqDelegate = 0x00000001,
		AscReqMutualAuth = 0x00000002,
		AscReqReplayDetect = 0x00000004,
		AscReqSequenceDetect = 0x00000008,
		AscReqConfidentiality = 0x00000010,
		AscReqUseSessionKey = 0x00000020,
		AscReqAllocateMemory = 0x00000100,
		AscReqUseDceStyle = 0x00000200,
		AscReqDatagram = 0x00000400,
		AscReqConnection = 0x00000800,
		AscReqCallLevel = 0x00001000,
		AscReqExtendedError = 0x00008000,
		AscReqStream = 0x00010000,
		AscReqIntegrity = 0x00020000,
		AscReqLicensing = 0x00040000,
		AscReqIdentify = 0x00080000,
		AscReqAllowNullSession = 0x00100000,
		AscReqAllowNonUserLogons = 0x00200000,
		AscReqAllowContextReplay = 0x00400000,
		AscReqFragmentToFit = 0x00800000,
		AscReqFragmentSupplied = 0x00002000,
		AscReqNoToken = 0x01000000,
	}

	public enum ContextAttr
	{
		AscRetDelegate = 0x00000001,
		AscRetMutualAuth = 0x00000002,
		AscRetReplayDetect = 0x00000004,
		AscRetSequenceDetect = 0x00000008,
		AscRetConfidentiality = 0x00000010,
		AscRetUseSessionKey = 0x00000020,
		AscRetAllocatedMemory = 0x00000100,
		AscRetUsedDceStyle = 0x00000200,
		AscRetDatagram = 0x00000400,
		AscRetConnection = 0x00000800,
		AscRetCallLevel = 0x00002000,
		AscRetThirdLegFailed = 0x00004000,
		AscRetExtendedError = 0x00008000,
		AscRetStream = 0x00010000,
		AscRetIntegrity = 0x00020000,
		AscRetLicensing = 0x00040000,
		AscRetIdentify = 0x00080000,
		AscRetNullSession = 0x00100000,
		AscRetAllowNonUserLogons = 0x00200000,
		AscRetFragmentOnly = 0x00800000,
		AscRetNoToken = 0x01000000,
	}

	public enum BufferType
	{
		SecbufferVersion = 0,

		SecbufferEmpty = 0,				// Undefined, replaced by provider
		SecbufferData = 1,					// Packet data
		SecbufferToken = 2,				// Security token
		SecbufferPkgParams = 3,			// Package specific parameters
		SecbufferMissing = 4,				// Missing Data indicator
		SecbufferExtra = 5,				// Extra data
		SecbufferStreamTrailer = 6,		// Security Trailer
		SecbufferStreamHeader = 7,		// Security Header
		SecbufferNegotiationInfo = 8,		// Hints from the negotiation pkg
		SecbufferPadding = 9,				// non-data padding
		SecbufferStream = 10,				// whole encrypted message
		SecbufferMechlist = 11,
		SecbufferMechlistSignature = 12,
		SecbufferTarget = 13,				// obsolete
		SecbufferChannelBindings = 14,
		SecbufferChangePassResponse = 15,
	}

	public enum UlAttribute
	{
		SecpkgAttrSizes = 0,
		SecpkgAttrNames = 1,
		SecpkgAttrLifespan = 2,
		SecpkgAttrDceInfo = 3,
		SecpkgAttrStreamSizes = 4,
		SecpkgAttrKeyInfo = 5,
		SecpkgAttrAuthority = 6,
		SecpkgAttrProtoInfo = 7,
		SecpkgAttrPasswordExpiry = 8,
		SecpkgAttrSessionKey = 9,
		SecpkgAttrPackageInfo = 10,
		SecpkgAttrUserFlags = 11,
		SecpkgAttrNegotiationInfo = 12,
		SecpkgAttrNativeNames = 13,
		SecpkgAttrFlags = 14,
		// These attributes exist only in Win XP and greater
		SecpkgAttrUseValidated = 15,
		SecpkgAttrCredentialName = 16,
		SecpkgAttrTargetInformation = 17,
		SecpkgAttrAccessToken = 18,
		// These attributes exist only in Win2K3 and greater
		SecpkgAttrTarget = 19,
		SecpkgAttrAuthenticationId = 20,
		// These attributes exist only in Win2K3SP1 and greater
		SecpkgAttrLogoffTime = 21,
	}

	public enum SchProtocols
	{
		ClientMask = -2147483478,
		Pct = 3,
		PctClient = 2,
		PctServer = 1,
		ServerMask = 0x40000055,
		Ssl2 = 12,
		Ssl2Client = 8,
		Ssl2Server = 4,
		Ssl3 = 0x30,
		Ssl3Client = 0x20,
		Ssl3Server = 0x10,
		Ssl3Tls = 240,
		Tls = 0xc0,
		TlsClient = 0x80,
		TlsServer = 0x40,
		UniClient = -2147483648,
		Unified = -1073741824,
		UniServer = 0x40000000,
		Zero = 0
	}

	#endregion

	#region Structs

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SchannelCred
	{
		public const int CurrentVersion = 4;
		public int version;
		public int cCreds;
		public IntPtr paCreds1;
		private readonly IntPtr rootStore;
		public int cMappers;
		private readonly IntPtr phMappers;
		public int cSupportedAlgs;
		private readonly IntPtr palgSupportedAlgs;
		public SchProtocols grbitEnabledProtocols;
		public int dwMinimumCipherStrength;
		public int dwMaximumCipherStrength;
		public int dwSessionLifespan;
		public Flags dwFlags;
		public int reserved;

		public SchannelCred(X509Certificate certificate, SchProtocols protocols)
			: this(CurrentVersion, certificate, SchannelCred.Flags.Zero, protocols)
		{
		}

		public SchannelCred(int version1, X509Certificate certificate, Flags flags, SchProtocols protocols)
		{
			paCreds1 = IntPtr.Zero;
			rootStore = phMappers = palgSupportedAlgs = IntPtr.Zero;
			cCreds = cMappers = cSupportedAlgs = 0;
			dwMinimumCipherStrength = dwMaximumCipherStrength = 0;
			dwSessionLifespan = reserved = 0;
			version = version1;
			dwFlags = flags;
			grbitEnabledProtocols = protocols;
			if (certificate != null)
			{
				paCreds1 = certificate.Handle;
				cCreds = 1;
			}
		}

		[Flags]
		public enum Flags
		{
			NoDefaultCred = 0x10,
			NoNameCheck = 4,
			NoSystemMapper = 2,
			ValidateAuto = 0x20,
			ValidateManual = 8,
			Zero = 0
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgInfo
	{
		public int fCapabilities;
		public short wVersion;
		public short wRPCID;
		public int cbMaxToken;
		IntPtr Name;
		IntPtr Comment;

		public string GetName()
		{
			return (Name != IntPtr.Zero) ? Marshal.PtrToStringAnsi(Name) : null;
		}

		public string GetComment()
		{
			return (Comment != IntPtr.Zero) ? Marshal.PtrToStringAnsi(Comment) : null;
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SecHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CredHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CtxtHandle
	{
		IntPtr dwLower;
		IntPtr dwUpper;

		public bool IsInvalid
		{
			get { return dwLower == IntPtr.Zero && dwUpper == IntPtr.Zero; }
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct SecBuffer
	{
		internal int cbBuffer;
		internal int BufferType;
		internal IntPtr pvBuffer;

		public SecBuffer(BufferType type, int count, IntPtr buffer)
		{
			BufferType = (int)type;
			cbBuffer = count;
			pvBuffer = buffer;
		}
	}

	public struct SecBufferEx
	{
		public BufferType _BufferType;
		public int _Size;
		public int _Offset;
		public object _Buffer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct SecBufferDesc
	{
		internal int ulVersion;
		internal int cBuffers;
		internal IntPtr pBuffers;
	}

	public unsafe class SecBufferDescEx
	{
		internal SecBufferDesc _SecBufferDesc;
		internal SecBuffer[] _SecBuffers;

		private GCHandle[] _handles;
		private GCHandle _descHandle;

		public SecBufferEx[] _Buffers;

		public SecBufferDescEx(SecBufferEx[] buffers)
		{
			_SecBufferDesc.ulVersion = (int)BufferType.SecbufferVersion;
			_SecBufferDesc.cBuffers = 0;
			_SecBufferDesc.pBuffers = IntPtr.Zero;
			_handles = null;
			_SecBuffers = null;
			_Buffers = buffers;
		}

		public int GetBufferIndex(BufferType type, int from)
		{
			for (int i = from; i < _Buffers.Length; i++)
				if (_Buffers[i]._BufferType == type)
					return i;
			return -1;
		}

		internal void Pin()
		{
			if (_SecBuffers == null || _SecBuffers.Length != _Buffers.Length)
			{
				_SecBuffers = new SecBuffer[_Buffers.Length];
				_handles = new GCHandle[_Buffers.Length];
			}

			for (int i = 0; i < _Buffers.Length; i++)
			{
				if (_Buffers[i]._Buffer != null)
					_handles[i] = GCHandle.Alloc(_Buffers[i]._Buffer, GCHandleType.Pinned);

				_SecBuffers[i].BufferType = (int)_Buffers[i]._BufferType;
				_SecBuffers[i].cbBuffer = _Buffers[i]._Size;

				if (_Buffers[i]._Buffer == null)
					_SecBuffers[i].pvBuffer = IntPtr.Zero;
				else
					_SecBuffers[i].pvBuffer = AddToPtr(_handles[i].AddrOfPinnedObject(), _Buffers[i]._Offset);
			}

			_descHandle = GCHandle.Alloc(_SecBuffers, GCHandleType.Pinned);

			_SecBufferDesc.ulVersion = (int)BufferType.SecbufferVersion;
			_SecBufferDesc.cBuffers = _SecBuffers.Length;
			_SecBufferDesc.pBuffers = _descHandle.AddrOfPinnedObject();
		}

		internal void Free()
		{
			object buffer = _Buffers[0]._Buffer;
			IntPtr bufferPtr = _handles[0].AddrOfPinnedObject();

			for (int i = 0; i < _Buffers.Length; i++)
			{
				_Buffers[i]._BufferType = (BufferType)_SecBuffers[i].BufferType;
				_Buffers[i]._Size = _SecBuffers[i].cbBuffer;

				if (_Buffers[i]._Size == 0 || _Buffers[i]._BufferType == BufferType.SecbufferEmpty)
				{
					_Buffers[i]._Buffer = null;
					_Buffers[i]._Offset = 0;
				}
				else
				{
					_Buffers[i]._Buffer = buffer;
					if (_SecBuffers[i].pvBuffer != IntPtr.Zero)
						_Buffers[i]._Offset = SubPtr(bufferPtr, _SecBuffers[i].pvBuffer);
					else
					{
						// FIX: AcceptSecurityContext returns zero pointer for extra data
						// It looks like AcceptSecurityContext do not ready for extra data
						// if (i == 1 && Buffers.Length == 2)
						// {
						//     Buffers[i].Offset = Buffers[i - 1].Offset
						//         + Buffers[i - 1].Size - Buffers[i].Size;
						// }
						// else
						// throw new InvalidOperationException("I do not know ho to fix SSPI WinApi return data");
					}
				}
			}

			for (int i = 0; i < _Buffers.Length; i++)
			{
				if (_handles[i].IsAllocated)
					_handles[i].Free();
			}

			_descHandle.Free();
		}

		private int SubPtr(IntPtr begin, IntPtr current)
		{
			return (int)((long)current - (long)begin);
		}

		private IntPtr AddToPtr(IntPtr begin, int offset)
		{
			return (IntPtr)((long)begin + offset);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgContextSizes
	{
		public int cbMaxToken;
		public int cbMaxSignature;
		public int cbBlockSize;
		public int cbSecurityTrailer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct SecPkgContextNames
	{
		public IntPtr sUserName;
	}

	[StructLayout(LayoutKind.Sequential)]
	struct SecPkgContextStreamSizes
	{
		public int cbHeader;
		public int cbTrailer;
		public int cbMaximumMessage;
		public int cBuffers;
		public int cbBlockSize;
	};

	#endregion

	#region SafeHandles

	[SuppressUnmanagedCodeSecurity]
	public class SafeCredHandle : SafeHandle
	{
		internal CredHandle _Handle;

		public SafeCredHandle(CredHandle credHandle)
			: base(IntPtr.Zero, true)
		{
			this._Handle = credHandle;
		}

		public override bool IsInvalid
		{
			get { return _Handle.IsInvalid; }
		}

		protected override bool ReleaseHandle()
		{
			return Secur32Dll.FreeCredentialsHandle(ref _Handle) == 0;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	public class SafeCtxtHandle : SafeHandle
	{
		internal CtxtHandle _Handle;

		public SafeCtxtHandle()
			: base(IntPtr.Zero, true)
		{
		}

		public SafeCtxtHandle(CtxtHandle ctxtHandle)
			: base(IntPtr.Zero, true)
		{
			this._Handle = ctxtHandle;
		}

		public override bool IsInvalid
		{
			get { return _Handle.IsInvalid; }
		}

		protected override bool ReleaseHandle()
		{
			return Secur32Dll.DeleteSecurityContext(ref _Handle) == 0;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	public class SafeContextBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		public SafeContextBufferHandle()
			: base(true)
		{
		}

		public SafeContextBufferHandle(IntPtr handle)
			: base(true)
		{
			SetHandle(handle);
		}

		override protected bool ReleaseHandle()
		{
			return Secur32Dll.FreeContextBuffer(handle) == 0;
		}

		public SecPkgInfo GetItem<T>(int index)
		{
			var address = (IntPtr)(DangerousGetHandle().ToInt64()
				+ Marshal.SizeOf(typeof(T)) * index);

			return (SecPkgInfo)Marshal.PtrToStructure(address, typeof(T));
		}
	}

	#endregion

    [Serializable]
	class SspiException : Win32Exception
	{
		public SspiException(int error, string function)
			: base(error, string.Format(@"SSPI error, function call {0} return 0x{1:x8}", function, error))
		{
		}

		public SecurityStatus SecurityStatus
		{
			get { return Sspi.Convert(base.ErrorCode); }
		}
	}

	static class Sspi
	{
		public static bool Succeeded(SecurityStatus result)
		{
			return (int)result >= 0;
		}

		public static bool Failed(SecurityStatus result)
		{
			return (int)result < 0;
		}

		internal static bool Succeeded(int result)
		{
			return result >= 0;
		}

		internal static bool Failed(int result)
		{
			return result < 0;
		}

		public static SecurityStatus EnumerateSecurityPackages(out int packages, out SafeContextBufferHandle secPkgInfos)
		{
			return Convert(
				Secur32Dll.EnumerateSecurityPackagesA(out packages, out secPkgInfos));
		}

		public static unsafe void AcquireCredentialsHandle(
			CredentialUse credentialUse,
			SchannelCred authData,
			out SafeCredHandle credential,
			out long expiry)
		{
			CredHandle handle;
			GCHandle paCredHandle = new GCHandle();
			IntPtr[] paCred = null;

			if (authData.cCreds > 0)
			{
				paCred = new IntPtr[] { authData.paCreds1 };
				paCredHandle = GCHandle.Alloc(paCred, GCHandleType.Pinned);
				authData.paCreds1 = paCredHandle.AddrOfPinnedObject();
			}

			try
			{
				int error = Secur32Dll.AcquireCredentialsHandleA(
					null,
					Secur32Dll.UnispName,
					(int)credentialUse,
					null,
					&authData,
					null,
					null,
					out handle,
					out expiry);

				credential = new SafeCredHandle(handle);

				if (error != 0)
					throw new SspiException(error, @"AcquireCredentialsHandleA");
			}
			finally
			{
				if (paCredHandle.IsAllocated)
					paCredHandle.Free();

				if (paCred != null)
					authData.paCreds1 = paCred[0];
			}
		}

		public static unsafe SecurityStatus SafeAcceptSecurityContext(
			ref SafeCredHandle credential,
			ref SafeCtxtHandle context,
			ref SecBufferDescEx input,
			int contextReq,
			TargetDataRep targetDataRep,
			ref SafeCtxtHandle newContext,
			ref SecBufferDescEx output,
			out int contextAttr,
			out long timeStamp)
		{
			try
			{
				input.Pin();
				output.Pin();

				fixed (void* fixedContext = &context._Handle)
				{
					int error = Secur32Dll.AcceptSecurityContext(
						ref credential._Handle,
						(context.IsInvalid) ? null : fixedContext,
						ref input._SecBufferDesc,
						contextReq,
						(int)targetDataRep,
						ref newContext._Handle,
						ref output._SecBufferDesc,
						out contextAttr,
						out timeStamp);

					return Convert(error);
				}
			}
			catch
			{
				contextAttr = 0;
				timeStamp = 0;
				return SecurityStatus.SecEUnknowError;
			}
			finally
			{
				input.Free();
				output.Free();
			}
		}

		public unsafe static SecurityStatus SafeDecryptMessage(
			ref SafeCtxtHandle context,
			ref SecBufferDescEx message,
			uint messageSeqNo,
			void* pfQop)
		{
			try
			{
				message.Pin();

				int error = Secur32Dll.DecryptMessage(
					ref context._Handle,
					ref message._SecBufferDesc,
					messageSeqNo,
					pfQop);

				return Convert(error);
			}
			catch
			{
				return SecurityStatus.SecEUnknowError;
			}
			finally
			{
				message.Free();
			}
		}

		public unsafe static void EncryptMessage(
			ref SafeCtxtHandle context,
			ref SecBufferDescEx message,
			uint messageSeqNo,
			void* pfQop)
		{
			try
			{
				message.Pin();

				int error = Secur32Dll.EncryptMessage(
					ref context._Handle,
					pfQop,
					ref message._SecBufferDesc,
					messageSeqNo);

				if (error != 0)
					throw new SspiException(error, @"EncryptMessage");
			}
			finally
			{
				message.Free();
			}
		}

		public unsafe static void QueryContextAttributes(
			ref SafeCtxtHandle context,
			out SecPkgContextStreamSizes streamSizes)
		{
			fixed (void* buffer = &streamSizes)
				QueryContextAttributes(ref context, UlAttribute.SecpkgAttrStreamSizes, buffer);
		}

		public unsafe static void QueryContextAttributes(
			ref SafeCtxtHandle context,
			UlAttribute attribute,
			void* buffer)
		{
			int error = Secur32Dll.QueryContextAttributesA(
				ref context._Handle,
				(uint)attribute,
				buffer);

			if (error != 0)
				throw new SspiException(error, @"QueryContextAttributesA");
		}

		public unsafe static SecurityStatus SafeQueryContextAttributes(
			ref SafeCtxtHandle context,
			out SecPkgContextStreamSizes streamSizes)
		{
			fixed (void* buffer = &streamSizes)
				return SafeQueryContextAttributes(ref context, UlAttribute.SecpkgAttrStreamSizes, buffer);
		}

		public unsafe static SecurityStatus SafeQueryContextAttributes(
			ref SafeCtxtHandle context,
			UlAttribute attribute,
			void* buffer)
		{
			try
			{
				int error = Secur32Dll.QueryContextAttributesA(
					ref context._Handle,
					(uint)attribute,
					buffer);

				return Convert(error);
			}
			catch
			{
				return SecurityStatus.SecEUnknowError;
			}
		}

		public static SecurityStatus Convert(int error)
		{
			if (Enum.IsDefined(typeof(SecurityStatus), (uint)error))
				return (SecurityStatus)error;
			return SecurityStatus.SecEUnknowError;
		}
	}

	[SuppressUnmanagedCodeSecurity]
	static class Secur32Dll
	{
		private const string Secur32 = @"secur32.dll";
		public const string UnispName = "Microsoft Unified Security Protocol Provider";

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int FreeContextBuffer(
			[In] IntPtr pvContextBuffer);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int FreeCredentialsHandle(
			[In] ref CredHandle phCredential);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		public static extern int EnumerateSecurityPackagesA(
			[Out] out int pcPackages,
			[Out] out SafeContextBufferHandle ppPackageInfo);

		[DllImport(Secur32, ExactSpelling = true, CharSet = CharSet.Ansi, BestFitMapping = false, ThrowOnUnmappableChar = true, SetLastError = true)]
		public unsafe static extern int AcquireCredentialsHandleA(
			[In, MarshalAs(UnmanagedType.LPStr)] string pszPrincipal,
			[In, MarshalAs(UnmanagedType.LPStr)] string pszPackage,
			[In] int fCredentialUse,
			[In] void* pvLogonId,
			[In] void* pAuthData,
			[In] void* pGetKeyFn,
			[In] void* pvGetKeyArgument,
			[Out] out CredHandle phCredential,
			[Out] out long ptsExpiry);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int AcceptSecurityContext(
			[In] ref CredHandle phCredential,
			[In, Out] void* phContext,
			[In] ref SecBufferDesc pInput,
			[In] int fContextReq,
			[In] int targetDataRep,
			[In, Out] ref CtxtHandle phNewContext,
			[In, Out] ref SecBufferDesc pOutput,
			[Out] out int pfContextAttr,
			[Out] out long ptsTimeStamp);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern int DeleteSecurityContext(
			[In] ref CtxtHandle phContext);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int QueryContextAttributesA(
			[In] ref CtxtHandle phContext,
			[In] uint ulAttribute,
			[Out] void* pBuffer);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int MakeSignature(
			[In] ref SecHandle phContext,
			[In] int fQop,
			[In, Out] ref SecBufferDesc pMessage,
			[In] int messageSeqNo);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int VerifySignature(
			[In] ref SecHandle phContext,
			[In] ref SecBufferDesc pMessage,
			[In] int messageSeqNo,
			[Out] out int pfQop);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static extern int DecryptMessage(
			[In] ref CtxtHandle phContext,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint messageSeqNo,
			[Out] out uint pfQop);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int DecryptMessage(
			[In] ref CtxtHandle phContext,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint messageSeqNo,
			[Out] void* pfQop);

		[DllImport(Secur32, ExactSpelling = true, SetLastError = true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe static extern int EncryptMessage(
			[In] ref CtxtHandle phContext,
			[Out] void* pfQop,
			[In, Out] ref SecBufferDesc pMessage,
			[In] uint messageSeqNo);
	}
}
