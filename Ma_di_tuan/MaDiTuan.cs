using System;
using System.Collections.Generic;
using System.Text;

namespace Ma_di_tuan
{
    class MaDiTuan
    {
        //Khai báo biến
        private int kt;//Kích thước bàn cờ
        private int x, y;//Tọa độ xuất phát ban đầu x,y
        private int[,] dd = new int[2501, 2501];
        private int[,] kn = new int[2501, 2501];
        private int[,] vt = new int[3, 2501];
        public int[,] _vt = new int[3, 2501];//Tạm thời
        private int[,] di = new int[3, 9];
        private bool ngung = false;
        //Các phương thức
        public int getVT(int _i,int _j)
        {
            return vt[_i,_j];
        }
        public void Set(int _kt,int _X,int _Y) 
        {
            kt=_kt;
            x = _X;
            y = _Y;
        }
        //Kiểm tra khả năng đi
        public bool TimDuong()
        {
            khoiTao();
            dd[x, y] = 1; 
            xuly(1, x, y);
            if (ngung) return true;// cout << "\nKet thuc.";
            else return false;// cout << "\nKhong co duong di";
        }
        public int ktkn(int i,int j)
        {
            int dem = 0;
            for(int l=1;l<=8;l++)
                if (i + di[1,l] >= 1 && i + di[1,l] <= kt && j + di[2,l] >= 1 && j + di[2,l] <= kt) dem++;
            return dem;
        }
        public void khoiTao()
        {
            //khoi tao
            for (int i = 1; i <= kt * kt; i++)
            {
                vt[1, i] = vt[2, i] = 0;
                _vt[1, i] = _vt[2, i] = 0;
            }
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++) dd[i,j] = 0;
            di[1,1] = -1; di[2,1] = -2;
            di[1,2] = 1; di[2,2] = -2;
            di[1,3] = -1; di[2,3] = 2;
            di[1,4] = 1; di[2,4] = 2;
            di[1,5] = -2; di[2,5] = -1;
            di[1,6] = -2; di[2,6] = 1;
            di[1,7] = 2; di[2,7] = -1;
            di[1,8] = 2; di[2,8] = 1;
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++) kn[i,j] = ktkn(i, j);
        }
        public int kiemtra()
        {
            for (int i = 1; i <= kt; i++)
                for (int j = 1; j <= kt; j++) if (dd[i,j] == 0) return 0;
            return 1;
        }
        public void xuly(int _i,int _x,int _y)
        {
            if(_i==kt*kt && ngung==false){
                ngung=true;
                if (kiemtra() == 1)
                {
                    for (int j = 1; j <= _i - 1; j++)
                    {
                        _vt[1, j] = vt[1, j];
                        _vt[2, j] = vt[2, j];
                    }
                }
            }
            else if(ngung==false){
                int[,] _di = new int[3, 9];
                int dem=0,i,j,l;
                //tim cac duong di tiep
                for( l=1;l<=8;l++)
                    if(_x+di[1,l]>=1 && _x+di[1,l]<=kt && _y+di[2,l]>=1 && _y+di[2,l]<=kt && dd[(_x+di[1,l]),(_y+di[2,l])]==0){
                        dem++;
                        _di[0,dem]=kn[ (_x+di[1,l]) ,(_y+di[2,l])];
                        _di[1,dem]=_x+di[1,l];
                        _di[2,dem]=_y+di[2,l];
                    }
                //sap xep theo kha nang
                int[] temp=new int[3];
                for( i=2;i<=dem;i++)
                    for( j=dem;j>=i;j--)
                    if(_di[0,j]<_di[0,j-1]){
                            temp[0]=_di[0,j];temp[1]=_di[1,j];temp[2]=_di[2,j];
                            _di[0,j]=_di[0,j-1];_di[1,j]=_di[1,j-1];_di[2,j]=_di[2,j-1];
                            _di[0,j-1]=temp[0];_di[1,j-1]=temp[1];_di[2,j-1]=temp[2];
                    }
                //Giam kha nang
                for(i=1;i<=dem;i++)
                    kn[(_di[1,i]),(_di[2,i])]--;
                kn[_x,_y]--;
                //bat dau duyet
                for(i=1;i<=dem;i++){
                    //danh dau
                    dd[(_di[1,i]),(_di[2,i])]=1;
                    //vi tri tiep theo
                    vt[1,_i]=_di[1,i];
                    vt[2,_i]=_di[2,i];
                    //goi xu ly cho vi tri tiep theo
                    xuly(_i+1,_di[1,i],_di[2,i]);
                    //bo danh dau
                    dd[(_di[1,i]),(_di[2,i])]=0;
                }
                //Tang lai kha nang
                for(i=1;i<=dem;i++)
                    kn[(_di[1,i]),(_di[2,i])]++;
                kn[_x,_y]++;
            }
        }
    }
}
