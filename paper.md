假设只有 1 和 2 牌，每张牌出现 1 的概率是 $p$，出 2 的概率是 $1-p$。假设只有白色，分数均为 $1$。设第 $i$ 轮期望获得 $n_i$ 张牌。策略是，前面所有轮把所有 1 牌用掉，2 牌不管，最后一轮用 2 牌加分。有 $n_{i+1}=(1+p)n_i$，因此 $n_m=(1+p)^{m-1}n_1$。计算 $\sum_{i=0}^{n_m}\dbinom{n_m}{i}p^i(1-p)^{n_m-i}2^{n_m-i}=(2-p)^{n_m}=(2-p)^{n_1(1+p)^{m-1}}$，这是最后一轮得分的期望。代入 $n_1=5,m=5,p=0.5$，算出来期望是 $25251$