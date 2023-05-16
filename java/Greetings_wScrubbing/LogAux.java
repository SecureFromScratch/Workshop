public final class LogAux
{
	public static String Scrub(String a_str) {
		StringBuilder sb = new StringBuilder();
		for (int i = 0 ; i < a_str.length() ; ++i) {
			sb.append(Character.isISOControl(a_str.charAt(i)) ? '?' : a_str.charAt(i));
		}
		return sb.toString();
	}
	
	public static String Obscure(String a_str) {
		if (a_str.length() < 5) {
			return a_str.charAt(0) + "***" + a_str.charAt(a_str.length() - 1);
		}
		StringBuilder sb = new StringBuilder();
		sb.append(a_str.charAt(0));
		for (int i = 1 ; i < a_str.length() - 1 ; ++i) {
			sb.append('*');
		}
		sb.append(a_str.charAt(a_str.length() - 1));
		return sb.toString();
	}
	
	public static String ObscureUsername(String a_str) {
		String[] parts = a_str.split("\\\\");
		if (parts.length == 1) {
			return Obscure(a_str);
		}
		return Obscure(parts[0]) + "\\" + Obscure(parts[1]);
	}
}