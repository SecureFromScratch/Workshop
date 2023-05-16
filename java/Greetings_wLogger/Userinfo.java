import java.lang.System;
import java.util.Iterator;
import java.util.Properties;
import java.util.Set;


public final class Userinfo
{
	public static String getUsername() {
		return System.getProperty("user.name");
	}
	
	public static String getDomainName() {
	    return System.getenv("USERDOMAIN");
	}
}
