export interface Product {
    productId:              number;
    name:                   string;
    productNumber:          string;
    color:                  Color | null;
    standardCost:           number;
    listPrice:              number;
    size:                   null | string;
    weight:                 number | null;
    productCategoryId:      number;
    productModelId:         number;
    sellStartDate:          Date;
    sellEndDate:            Date | null;
    discontinuedDate:       null;
    thumbNailPhoto:         string;
    thumbnailPhotoFileName: string;
    rowguid:                string;
    modifiedDate:           Date;
    productCategory:        null;
    productModel:           null;
    salesOrderDetails:      any[];
}

export enum Color {
    Black = "Black",
    Blue = "Blue",
    Grey = "Grey",
    Multi = "Multi",
    Red = "Red",
    Silver = "Silver",
    SilverBlack = "Silver/Black",
    White = "White",
    Yellow = "Yellow",
}
