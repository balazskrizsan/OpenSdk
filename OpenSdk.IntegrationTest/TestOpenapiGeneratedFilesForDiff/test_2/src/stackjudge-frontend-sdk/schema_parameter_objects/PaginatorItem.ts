import {IOpenSdkGetable} from "../../common/intercaes/IOpenSdkGetable";
import {IOpenSdkPostable} from "../../common/intercaes/IOpenSdkPostable";
import * as valueObject from "./index";

/**
* @Generated("OpenSDK: https://github.com/balazskrizsan/OpenSdk")
*/
export class PaginatorItem
{
    public constructor(
        private typeId: string,
        private pageNumber: string,
        private navigation: string,
        private active: boolean,
    ) {
    }
}
