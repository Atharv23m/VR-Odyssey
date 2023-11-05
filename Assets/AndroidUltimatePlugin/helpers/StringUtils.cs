using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringUtils
{
   public static bool IsSameUrl(string url1, string url2)
    {
        Uri u1 = new Uri(url1);
        Uri u2 = new Uri(url2);
        return u1.Equals(u2);
    }

	public static int compareURL(string url1, string url2){
		int r = string.Compare(url1, url2);
		return r;
	}

	public static int compareURLOrdinal(string url1, string url2){
		int r = string.CompareOrdinal(url1, url2);
		return r;
	}

	public static bool normalCompare(string a, string b){
		if (a.Equals(b, StringComparison.Ordinal)){
			return true;
		}else{
			return false;
		}
	}
}
