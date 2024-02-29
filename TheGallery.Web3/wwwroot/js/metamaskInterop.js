async function sendTransactionViaMetaMask(transactionParameters) {
    try {
        const txHash = await ethereum.request({
            method: 'bid',
            params: [transactionParameters],
        });
        return txHash;
    } catch (error) {
        console.error("Erreur lors de l'envoi de la transaction via MetaMask:", error);
        throw error;
    }
}

async function callBidFunction(contractAddress, abi, bidLimit, offerAmount) {
    if (typeof window.ethereum !== 'undefined' && window.web3) {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedAbi = typeof abi === 'string' ? JSON.parse(abi) : abi;
            const contract = new web3.eth.Contract(parsedAbi, contractAddress);
            const data = contract.methods.bid(bidLimit).encodeABI();

            const transactionParameters = {
                to: contractAddress,
                from: ethereum.selectedAddress,
                value: web3.utils.toHex(offerAmount),
                data: data,
            };

            const txHash = await ethereum.request({
                method: 'eth_sendTransaction',
                params: [transactionParameters],
            });

            return txHash;
        } catch (error) {
            console.error("Erreur lors de l'appel de la fonction bid :", error);
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function callRefuseLastOfferFunction(contractAddress, abi) {
    if (typeof window.ethereum !== 'undefined' && window.web3) {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedAbi = typeof abi === 'string' ? JSON.parse(abi) : abi;
            const contract = new web3.eth.Contract(parsedAbi, contractAddress);
            const data = contract.methods.refuseLastOffer().encodeABI();
            const transactionParameters = {
                to: contractAddress,
                from: ethereum.selectedAddress,
                value: '0x0',
                data: data,
            };
            const txHash = await ethereum.request({
                method: 'eth_sendTransaction',
                params: [transactionParameters],
            });
            return txHash;
        } catch (error) {
            console.error("Erreur lors de l'appel de la fonction refuseLastOffer :", error);
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function callCancelAuctionFunction(contractAddress, abi) {
    if (typeof window.ethereum !== 'undefined') {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedAbi = typeof abi === 'string' ? JSON.parse(abi) : abi;
            const contract = new web3.eth.Contract(parsedAbi, contractAddress);
            const data = contract.methods.cancelAuction().encodeABI();
            const transactionParameters = {
                to: contractAddress,
                from: ethereum.selectedAddress,
                value: '0x0',
                data: data,
            };
            
            const txHash = await ethereum.request({
                method: 'eth_sendTransaction',
                params: [transactionParameters],
            });
            return txHash;
        } catch (error) {
            console.error("Erreur lors de l'appel de la fonction cancelAuction :", error);
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function callAcceptLastOfferFunction(contractAddress, abi) {
    if (typeof window.ethereum !== 'undefined') {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedAbi = typeof abi === 'string' ? JSON.parse(abi) : abi;
            const contract = new web3.eth.Contract(parsedAbi, contractAddress);
            const data = contract.methods.acceptLastOffer().encodeABI();

            const transactionParameters = {
                to: contractAddress,
                from: ethereum.selectedAddress,
                value: '0x0',
                data: data,
            };
            
            const txHash = await ethereum.request({
                method: 'eth_sendTransaction',
                params: [transactionParameters],
            });

            return txHash;
        } catch (error) {
            console.error("Erreur lors de l'appel de la fonction acceptLastOffer :", error);
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function mintNFT(contractAddress, abi) {
    if (typeof window.ethereum !== 'undefined') {
        try {
            const web3 = new Web3(window.ethereum);
            await ethereum.request({ method: 'eth_requestAccounts' });
            const parsedAbi = typeof abi === 'string' ? JSON.parse(abi) : abi;
            const contract = new web3.eth.Contract(parsedAbi, contractAddress);
            const accounts = await web3.eth.getAccounts();
            const account = accounts[0];

            const tx = await contract.methods.mint().send({ from: account });
            return JSON.stringify(tx);
        } catch (error) {
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function approveNFT(contractAddress, nftContractAbi, auctionFactoryAddress, tokenId) {
    const web3 = new Web3(window.ethereum);
    try {
        await ethereum.request({ method: 'eth_requestAccounts' });
        const parsedAbi = typeof nftContractAbi === 'string' ? JSON.parse(nftContractAbi) : nftContractAbi;
        const nftContract = new web3.eth.Contract(parsedAbi, contractAddress);
        const accounts = await web3.eth.getAccounts();
        const account = accounts[0];
        const tx = await nftContract.methods.approve(auctionFactoryAddress, tokenId).send({ from: account });
        return JSON.stringify(tx);
    } catch (error) {
        throw error;
    }
}

async function newAuction(auctionFactoryContractAddress, auctionFactoryContractAbi, nftContractAddress, tokenId, floorPrice, reservePrice, coeff, buyNow, paymentSplitter) {
    if (typeof window.ethereum !== 'undefined') {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedAuctionFactoryAbi = typeof auctionFactoryContractAbi === 'string' ? JSON.parse(auctionFactoryContractAbi) : auctionFactoryContractAbi;
            const auctionFactoryContract = new web3.eth.Contract(parsedAuctionFactoryAbi, auctionFactoryContractAddress);
            const accounts = await web3.eth.getAccounts();
            const account = accounts[0];
            const tx = await auctionFactoryContract.methods.newAuction(nftContractAddress, tokenId, floorPrice, reservePrice, coeff, buyNow, paymentSplitter).send({ from: account });
            return JSON.stringify(tx);
        } catch (error) {
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function newSMAuction(SMAuctionFactoryContractAddress, SMAuctionFactoryContractAbi, nftContractAddress, tokenId, floorPrice, reservePrice, coeff, buyNow) {
    if (typeof window.ethereum !== 'undefined') {
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedSMAuctionFactoryAbi = typeof SMAuctionFactoryContractAbi === 'string' ? JSON.parse(SMAuctionFactoryContractAbi) : SMAuctionFactoryContractAbi;
            const SMauctionFactoryContract = new web3.eth.Contract(parsedSMAuctionFactoryAbi, SMAuctionFactoryContractAddress);
            const accounts = await web3.eth.getAccounts();
            const account = accounts[0];
            const tx = await SMauctionFactoryContract.methods.newAuction(nftContractAddress, tokenId, floorPrice, reservePrice, coeff, buyNow).send({ from: account });
            return JSON.stringify(tx);
        } catch (error) {
            throw error;
        }
    } else {
        throw new Error("MetaMask n'est pas disponible.");
    }
}

async function callReleaseFunction(PaymentSplitterAddress, PaymentSplitterContractAbi, address) {
    if (typeof window.ethereum !== 'undefined')
{
        try {
            await ethereum.request({ method: 'eth_requestAccounts' });
            const web3 = new Web3(ethereum);
            const parsedPaymentSplitterAbi = typeof PaymentSplitterContractAbi === 'string' ? JSON.parse(PaymentSplitterContractAbi) : PaymentSplitterContractAbi;
            const PaymentSplitterContract = new web3.eth.Contract(parsedPaymentSplitterAbi, PaymentSplitterAddress);
            const accounts = await web3.eth.getAccounts();
            const account = accounts[0];
            const tx = await PaymentSplitterContract.methods.release(address).send({ from: account });
            return JSON.stringify(tx);
        } catch (error) {
            throw error;
        }
    }
}