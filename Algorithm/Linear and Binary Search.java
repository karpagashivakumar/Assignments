import java.util.*;

class Main {
    public static void main(String args[]) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int a[] = new int[n];
        
        for (int i = 0; i < n; i++) {
            a[i] = in.nextInt();
        }
        
        // Linear Search
        
        int k = in.nextInt();
        boolean found = false;
        for (int i = 0; i < n; i++) {
            if (k == a[i]) {
                System.out.println("Element found at (original index): " + i);
                found = true;
                break;
            }
        }
        if (!found) {
            System.out.println("Element not found in the original array.");
        }
        
        // Binary Search
        
        Arrays.sort(a);
        int l = 0, r = n - 1;
        while (l <= r) {
            int m = l + (r - l) / 2; 

            if (a[m] == k) {
                System.out.println("Element found at (sorted index): " + m);
                break;
            } else if (a[m] > k) {
                r = m - 1;
            } else {
                l = m + 1;
            }
        }
    }
}
