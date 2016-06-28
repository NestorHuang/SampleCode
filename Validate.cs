using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validate
{
    public class CheckID
    {
        /// <summary>
        /// 公司統一編號驗證規則
        /// 1.公司統編與邏輯數相乘，邏輯數為 12121241
        ///     如：1 2 3 4 5 6 7 5
        ///         1 2 1 2 1 2 4 1
        ///   乘積：1 4 4 8 5 1 2 5
        ///                   2 8
        ///  乘積和：1 4 4 8 5 3 1 5
        ///                      0
        ///                      
        /// </summary>
        private static int[] COMPANY_ID_LOGIC_MULTIPLIER = { 1, 2, 1, 2, 1, 2, 4, 1 };
        public static bool CompanyID(string CompanyID)
        {
            try
            {
                if (!CompanyID.Length.Equals(8))
                    return false;
                int aSum = 0;
                for (int i = 0; i < COMPANY_ID_LOGIC_MULTIPLIER.Length; i++)
                {
                    //公司統編與邏輯乘數相乘.
                    int aMultiply = int.Parse(CompanyID.Substring(i, 1)) * COMPANY_ID_LOGIC_MULTIPLIER[i];

                    //將相乘的結果, 取十位數及個位數相加.
                    int aAddition = (aMultiply / 10) + (aMultiply % 10);

                    //如果公司統編的第 7 位是 7 時, 會造成相加結果為 10 的特殊情況, 所以直接以 1 代替進行加總.
                    aSum += (aAddition == 10) ? 1 : aAddition;
                }

                //判斷總和的餘數, 假使為 0 公司統編正確回傳 true, 其它值則反之.
                if (!CompanyID.Substring(6, 1).Equals("7"))
                    return (aSum % 10 == 0);
                else//如果公司統編的第 7 位是 7 時,aSum能被整除或aSum-1(預設取1)能被整除都成立
                    if (aSum % 10 == 0 || (aSum - 1) % 10 == 0)
                        return true;
                    else
                        return false;
            }
            catch (Exception)
            {
                //如果 aCompanyId 參數為 null, 或者不是八位數, 或為其它非數值字串, 均傳回 false.
                return false;
            }
        }
    }
}
