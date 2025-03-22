/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
import java.util.Stack;
import java.util.*;

class Main {
    static boolean isBalanced(String s) {  
        
        Stack<Character> st = new Stack<>();
        for (int i = 0; i < s.length(); i++) {
        
            if (s.charAt(i) == '(' || s.charAt(i) == '{' || s.charAt(i) == '[') {
                st.push(s.charAt(i)); 
            } 
            else {
            
                if (!st.empty() && 
                    ((st.peek() == '(' && s.charAt(i) == ')') ||
                     (st.peek() == '{' && s.charAt(i) == '}') ||
                     (st.peek() == '[' && s.charAt(i) == ']'))) {
                    st.pop(); 
                }
                else {
                
                    return false; 
                }
            }
        }
        return st.empty();
    }

    public static void main(String[] args) {
        Scanner in=new Scanner(System.in);
        String s = in.nextLine();
        if (isBalanced(s))
            System.out.println("String consist of balanced parantheses");
        else
            System.out.println("String does not contain balanced parantheses");
    }
}
